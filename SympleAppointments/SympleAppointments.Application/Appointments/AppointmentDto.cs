using SympleAppointments.Application.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SympleAppointments.Application.Appointments
{
    public class AppointmentDto
    {
        public Guid AppointmentId { get; set; }
        public string Worker { get; set; }
        public string Client { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public ICollection<AnnotationDto> Annotations { get; set; }
        public string Status { get; set; }
    }
}
