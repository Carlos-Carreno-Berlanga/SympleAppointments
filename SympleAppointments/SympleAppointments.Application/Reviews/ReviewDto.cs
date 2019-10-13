using System;

namespace SympleAppointments.Application.Reviews
{
    public class ReviewDto
    {
        public Guid ReviewId { get; set; }
        public int NumberOfStars { get; set; }
        public string Comment { get; set; }

        public string Username { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
