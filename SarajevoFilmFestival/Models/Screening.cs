namespace SarajevoFilmFestival.Models;

public class Screening
{
    public int id { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public decimal TicketPrice { get; set; }
    public int TotalCapacity { get; set; }
    public int AvaliableSeats { get; set; }
    public delegate string Formater(Screening screening);
    
}