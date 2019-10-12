namespace SympleAppointments.Domain
{
    public enum AppointmentStatus
    {
        Pending = 1,
        CancelledByClient = 2,
        CancelledByWorker = 3,
        Finished = 4
    }
}
