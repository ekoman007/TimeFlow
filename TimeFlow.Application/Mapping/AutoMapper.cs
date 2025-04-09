using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.DTOs;
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Application.Mapping
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            // Krijo mapimin nga AddUserDto në User
            CreateMap<AddUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))  // Mapimi i PasswordHash
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))  // Mapimi i Username
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))  // Mapimi i Email
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));  // Mapimi i RoleId
        }
    }
}