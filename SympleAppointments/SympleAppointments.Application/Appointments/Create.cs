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
    public class Create
    {
        public class Command : IRequest<AppointmentDto>
        {
            public string Worker { get; set; }
            public string Client { get; set; }

            public DateTime StartDateTime { get; set; }

            public DateTime EndDateTime { get; set; }

        }

        public class Handler : IRequestHandler<Command, AppointmentDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<AppointmentDto> Handle(Command request, CancellationToken cancellationToken)
            {
                AppUser worker = null;
                AppUser client = null;

                if (!string.IsNullOrEmpty(request.Worker))
                {
                    worker = await _context.Users.SingleOrDefaultAsync(x => x.Id == request.Worker);
                    if (_context.Users.FindAsync(request.Worker) == null)
                    {
                        throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });
                    }

                }

                if (!string.IsNullOrEmpty(request.Client))
                {
                    client = await _context.Users.SingleOrDefaultAsync(x => x.Id == request.Client);
                    if (_context.Users.FindAsync(request.Client) == null)
                    {
                        throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });
                    }

                }

                var appointment = new Appointment
                {
                    Client = client,
                    Worker = worker,
                    EndDateTime = request.EndDateTime,
                    StartDateTime = request.StartDateTime,
                    Status = AppointmentStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                    //Annotations= new List<Annotation>(){ new Annotation { Note="TTT" }, new Annotation { Note = "xxxx" } }

                };

                _context.Appointments.Add(appointment);

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return _mapper.Map<AppointmentDto>(appointment);
                }

                throw new Exception("Problem saving changes");
            }
        }
    }
}
