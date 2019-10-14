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

namespace SympleAppointments.Application.Reviews
{
    public class GetAll
    {
        public class Query : IRequest<List<ReviewDto>>
        {
            public Query()
            {

            }
        }

        public class Handler : IRequestHandler<Query, List<ReviewDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<List<ReviewDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var reviews = await _context.Reviews.Select(x => x).ToListAsync();
                return _mapper.Map<List<Review>, List<ReviewDto>>(reviews);
            }
        }

    }
}
