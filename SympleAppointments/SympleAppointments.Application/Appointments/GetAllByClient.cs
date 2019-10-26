using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SympleAppointments.Domain;
using SympleAppointments.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SympleAppointments.Application.Appointments
{
    public class GetAllByClient
    {
        public class Query : IRequest<List<AppointmentDto>>
        {
            public Guid ClientId { get; set; }

        }

        public class Handler : IRequestHandler<Query, List<AppointmentDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<List<AppointmentDto>> Handle(Query request, CancellationToken cancellationToken)
            {

                var reviews = await _context.Appointments.Where(x => x.Client.Id == request.ClientId.ToString())
                    .Select(x => x)
                    .Include(w => w.Worker)
                    .Include(c => c.Client)
                    .ToListAsync();
                return _mapper.Map<List<Appointment>, List<AppointmentDto>>(reviews);
            }
        }

    }
}