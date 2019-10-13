using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SympleAppointments.Application.Exceptions;
using SympleAppointments.Domain;
using SympleAppointments.Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SympleAppointments.Application.Reviews
{
    public class Create
    {
        public class Command : IRequest<ReviewDto>
        {
            public Guid ReviewId { get; set; }
            public int NumberOfStars { get; set; }
            public string Comment { get; set; }

            public string userId { get; set; }
        }

        public class Handler : IRequestHandler<Command, ReviewDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<ReviewDto> Handle(Command request, CancellationToken cancellationToken)
            {
                AppUser user = null;

                if (!string.IsNullOrEmpty(request.userId))
                {
                    user = await _context.Users.SingleOrDefaultAsync(x => x.Id == request.userId);
                    if (_context.Users.FindAsync(request.userId) == null)
                    {
                        throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });
                    }

                }

                var review = new Review
                {
                    AppUser = user,
                    CreatedAt= DateTime.UtcNow,
                    Comment= request.Comment,
                    NumberOfStars = request.NumberOfStars

                };

                _context.Reviews.Add(review);

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return _mapper.Map<ReviewDto>(review);
                }

                throw new Exception("Problem saving changes");
            }
        }
    }
}
