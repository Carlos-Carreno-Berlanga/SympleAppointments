using System;
using System.Collections.Generic;

namespace SympleAppointments.Domain
{
    public class Appointment
    {
        public Guid AppointmentId { get; set; }
        public AppUser Worker { get; set; }
        public AppUser Client { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public ICollection<Annotation> Annotations { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
