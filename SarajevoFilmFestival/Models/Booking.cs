namespace SarajevoFilmFestival.Models;

    public enum BookingStatus
    {
        Confirmed,
        Cancelled
    }

    public class Booking
    {
        public int ID { get; set; }
        public int CustomerID{get; set; }
        public int ScreeningID { get; set; }
        public int TicketCount { get; set; }
        public BookingStatus Status { get; set; }
        public int TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
