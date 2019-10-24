using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SympleAppointments.Application.Exceptions;
using SympleAppointments.Domain;
using SympleAppointments.Persistence;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SympleAppointments.Application.Appointments
{
    public class GetOne
    {
        public class Query : IRequest<AppointmentDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, AppointmentDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<AppointmentDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var appointment = await _context.Appointments.
                    Include(w => w.Worker).
                    Include(c => c.Client).
                    FirstOrDefaultAsync(x => x.AppointmentId == request.Id);

                if (appointment == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Review = "Not found" });

                var reviewToReturn = _mapper.Map<Appointment, AppointmentDto>(appointment);

                return reviewToReturn;
            }
        }
    }
}