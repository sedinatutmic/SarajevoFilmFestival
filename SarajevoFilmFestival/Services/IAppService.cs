using SarajevoFilmFestival.Models;

namespace SarajevoFilmFestival.Services;

public interface IAppService
{
    Screening AddScreening(string title, DateTime date, decimal ticketPrice, int totalCapacity, int avaliableSeats);
    
    Customer addCustomer(string name, string lastName, string email, int phoneNumber);
    Booking assignBookingToCustomer(int customerId, int screeningId, int ticketCount);

    Dictionary<int, List<Booking>> GroupBookingByScreening();
    decimal GetTotalRevenue();
    List<Booking> GetCancelledBookings();
    
    List<Customer>GetAllCustomers();
    List<Screening>GetAllScreenings();
    List<Booking>GetAllBookings();
}
