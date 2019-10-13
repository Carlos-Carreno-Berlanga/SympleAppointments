using AutoMapper;
using SympleAppointments.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SympleAppointments.Application.Reviews
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Review, ReviewDto>().
                ForMember(r => r.Username, o => o.MapFrom(u => u.AppUser.UserName));

        }
    }
}
