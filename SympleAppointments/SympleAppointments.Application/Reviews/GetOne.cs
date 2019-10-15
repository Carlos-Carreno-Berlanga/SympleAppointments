using AutoMapper;
using MediatR;
using SympleAppointments.Application.Exceptions;
using SympleAppointments.Domain;
using SympleAppointments.Persistence;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SympleAppointments.Application.Reviews
{
    public class GetOne
    {
        public class Query : IRequest<ReviewDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ReviewDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<ReviewDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var review = await _context.Reviews.
                    Include(r => r.AppUser).
                    FirstOrDefaultAsync(x => x.ReviewId == request.Id);

                if (review == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Review = "Not found" });

                var reviewToReturn = _mapper.Map<Review, ReviewDto>(review);

                return reviewToReturn;
            }
        }
    }
}
