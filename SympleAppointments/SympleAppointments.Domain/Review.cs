using System;
using System.Collections.Generic;
using System.Text;

namespace SympleAppointments.Domain
{
    public class Review
    {
        public Guid ReviewId { get; set; }
        public int NumberOfStars { get; set; }
        public string Comment { get; set; }

        public AppUser AppUser { get; set; }
    }
}
