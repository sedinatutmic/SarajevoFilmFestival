using SarajevoFilmFestival.Models;

namespace SarajevoFilmFestival.Services;

public interface IAppService
{
    Screening AddScreening(string title, int capacity, decimal ticketPrice, bool isActive);
    void ActivateScreening(int screeningId);
    void DeactivateScreening(int screeningId);

    Customer addCustomer(string name, string lastName, string email, int phoneNumber);
    Booking assignBookingToCustomer(int customerId, int screeningId, int ticketCount);

    Dictionary<int, List<Booking>> GroupBookingByScreening();
    Customer? GetCustomerWithMostTickets();
    List<Booking> GetCancelledBookings();
    
    List<Customer>GetAllCustomers();
    List<Screening>GetAllScreenings();
    List<Booking>GetAllBookings();
}
