using System;
using System.Collections.Generic;

namespace SympleAppointments.Domain
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public AppUser Worker { get; set; }
        public AppUser Client { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        ICollection<Annotation> Annotations { get; set; }
    }
}
