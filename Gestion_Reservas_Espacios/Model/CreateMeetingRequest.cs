namespace Gestion_Reservas_Espacios.Model
{
    public class CreateMeetingRequest
    {
        public DateTime StartHour { get; set; }

        public DateTime EndHour { get; set; }

        public string Description { get; set; }
    }
}
