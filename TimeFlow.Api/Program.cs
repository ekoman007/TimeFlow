using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using TimeFlow.Api.Middlewares;
using TimeFlow.Application.Commands;
using TimeFlow.Application.Commands.Roles;
using TimeFlow.Domain.Repositories;
using TimeFlow.Domain.Security;
using TimeFlow.Infrastructure.Contracts;
using TimeFlow.Infrastructure.Contracts.Roles;
using TimeFlow.Infrastructure.Database;
using TimeFlow.Infrastructure.Repositories;
using TimeFlow.Infrastructure.Repositories.Roles;
using TimeFlow.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

// Shto Swagger dhe konfigurimin e autorizimit me JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "AuthService API",
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

// Regjistro JWT token generator dhe sh�rbime t� tjera
builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

//builder.Services.AddSingleton<IUserRegisteredPublisher, UserRegisteredPublisher>();

// Regjistro AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//// Regjistro MediatR 
//builder.Services.AddMediatR(typeof(LoginUserCommand).Assembly);
//builder.Services.AddMediatR(typeof(AddUserCommand).Assembly);
builder.Services.AddMediatR(typeof(LoginCommand).Assembly);
builder.Services.AddMediatR(typeof(CreateRoleCommand).Assembly);

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
            ValidIssuer = "YourApp",
            ValidAudience = "YourApp",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
        };
    });

// Shtoni CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()    // Lejon �do origjin�
              .AllowAnyMethod()    // Lejon �do metod� (GET, POST, PUT, etj.)
              .AllowAnyHeader();   // Lejon �do header
    });
});

var app = builder.Build();

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

app.UseHttpsRedirection();

// Aktivizoni CORS
app.UseCors("AllowAll");  // Kjo p�rdor politik�n "AllowAll" q� krijuam m� par�

// Aktivizoni Authentication dhe Authorization
app.UseAuthentication(); // Kjo �sht� e nevojshme p�r t� validuar tokenin JWT
app.UseAuthorization();

app.MapControllers();

app.Run();
