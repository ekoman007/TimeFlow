using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Address.Commands
{
    public class UpdateAddressCommand : IRequest<GeneralResponse<int>>
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public bool IsPrimary { get; set; }
        public int ApplicationUserDetailsId { get; set; }
        public int BusinessProfileId { get; set; }
    }
}