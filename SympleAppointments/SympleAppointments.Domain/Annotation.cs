namespace SympleAppointments.Domain
{
    public class Annotation
    {
        public int AnnotationId { get; set; }

        public string Note { get; set; }

        public int AppointmentId { get; set; }
    }
}
