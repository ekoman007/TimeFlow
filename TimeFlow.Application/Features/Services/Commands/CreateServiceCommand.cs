using System;
using MediatR;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Services.Commands
{
    public class CreateServiceCommand : IRequest<GeneralResponse<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInMinutes { get; set; }
        public string? ServiceType { get; set; }
        public string? Tags { get; set; }
        public int? MaxBookingsPerDay { get; set; }
        public string? AdditionalInfo { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string? Availability { get; set; }
        public string? RequiredMaterials { get; set; }
        public string ServiceCode { get; set; }
        public string? Currency { get; set; }
        public string ImageUrl { get; set; }
        public int BusinessProfileId { get; set; }
    }
}
