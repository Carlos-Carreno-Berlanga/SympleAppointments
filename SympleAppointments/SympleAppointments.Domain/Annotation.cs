using System;

namespace SympleAppointments.Domain
{
    public class Annotation
    {
        public Guid AnnotationId { get; set; }

        public Appointment Appointment { get; set; }

        public string Note { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
