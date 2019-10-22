using AutoMapper;
using SympleAppointments.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SympleAppointments.Application.Appointments
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Appointment, AppointmentDto>().
                ForMember(r => r.Worker, o => o.MapFrom(u => u.Worker.UserName))
                .ForMember(r => r.Client, o => o.MapFrom(u => u.Client.UserName))
                ;

        }
    }
}