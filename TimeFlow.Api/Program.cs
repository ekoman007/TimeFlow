using Microsoft.EntityFrameworkCore;
using Serilog;
using TimeFlow.Api.Middlewares;
using TimeFlow.Infrastructure.Database; 
using TimeFlow.Api.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TimeFlow.SharedKernel;
using Hellang.Middleware.ProblemDetails; 

var builder = WebApplication.CreateBuilder(args);

// Regjistro sh�rbimet e aplikacionit
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithEnvironmentName());

builder.Services.AddProblemDetails(options =>
{
    options.IncludeExceptionDetails = (ctx, ex) => false; // nuk kthen stacktrace

    options.Map<DomainException>(ex => new Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        Title = "Validation Error",
        Detail = ex.Message,
        Status = StatusCodes.Status400BadRequest
    });

    options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
});



// Regjistro sh�rbimet e aplikacionit nga file-i i ve�ant�
builder.Services.AddApplicationServices();

// Shto Swagger dhe konfigurimin e autorizimit me JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TimeFlow API",
        Version = "v1"
    });

    // Shto p�rkufizimin p�r autorizimin JWT n� Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Shto konfigurimin e DbContext
builder.Services.AddDbContext<TimeFlowDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Konfigurimi i JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],  // P�rdorim vler�n nga appsettings.json
            ValidAudience = builder.Configuration["JwtSettings:Audience"], // P�rdorim vler�n nga appsettings.json
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
        };
    });

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));


// Shtoni CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("https://localhost:3000", "http://localhost:3000") // Specify your allowed origins
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});


var app = builder.Build();

await app.SeedDatabaseAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthService API V1");
        c.RoutePrefix = "swagger"; // e b�n t� hapet te /swagger/index.html
    });
}
app.UseProblemDetails(); // p�rdor middleware-in

app.UseHttpsRedirection();

// Aktivizoni CORS
app.UseCors("AllowAll");


app.UseAuthentication(); // Kjo �sht� e nevojshme p�r t� validuar tokenin JWT
app.UseAuthorization();
 

app.MapControllers();

app.Run();
