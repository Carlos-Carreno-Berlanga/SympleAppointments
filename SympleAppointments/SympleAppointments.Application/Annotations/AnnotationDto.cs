using System;
using System.Collections.Generic;
using System.Text;

namespace SympleAppointments.Application.Annotations
{
    public class AnnotationDto
    {
        public Guid AnnotationId { get; set; }

        public string Note { get; set; }
    }
}
