namespace Gestion_Reservas_Espacios.Model
{
    public class CreateReservationRequest
    {
        public int SpaceId { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }
    }
}
