using System.Runtime.InteropServices;
using SarajevoFilmFestival.Repositories;
using SarajevoFilmFestival.Models;

namespace SarajevoFilmFestival.Services;

public class FestivalService : IAppService
{
    private readonly Repository<Customer> _customerRepo;
    private readonly Repository<Booking> _bookingRepo;
    private readonly Repository<Screening> _screeningRepo;

    public FestivalService(
        Repository<Customer> customerRepo,
        Repository<Booking> bookingRepo,
        Repository<Screening> screeningRepo
    )
    {
        customerRepo=_customerRepo;
        bookingRepo=_bookingRepo;
        screeningRepo=_screeningRepo;
    }

    public Screening AddScreening(string title, DateTime date, decimal ticketPrice, int totalCapacity,
        int avaliableSeats)
    {
        if (totalCapacity <= 0 || totalCapacity > 500)
        {
            throw new ArgumentException("Total capacity must be greater than 0 and less than or equal to 500!");
        }

        if (ticketPrice <= 0)
        {
            throw new ArgumentException("Ticket price must be greater than 0!");
        }

        var newScreening = new Screening
        {
            Title = title,
            Date = date,
            TicketPrice = ticketPrice,
            TotalCapacity = totalCapacity,
            AvaliableSeats = avaliableSeats
        };
        _screeningRepo.Add(newScreening);
        return new Screening();
    }

    public Customer addCustomer(string name, string lastName, string email, int phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("First name and Last name can not be empty!");
        }

        var newCustomer = new Customer
        {
            Name = name,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber
        };
        
        _customerRepo.Add(newCustomer);
        return newCustomer;
    }

    public Booking assignBookingToCustomer(int customerId, int screeningId, int ticketCount)
    {
        var customer= _customerRepo.GetById(customerId);
        var screening = _screeningRepo.GetById(screeningId);

        if (customer == null || screening == null)
        {
            throw new InvalidOperationException("Customer or screening does not exist!");
        }

        if (ticketCount <= 0)
        {
            throw new ArgumentException("Ticket count must be greater than 0!");
        }

        if (screening.AvaliableSeats < ticketCount)
        {
            throw new InvalidOperationException("Capacity reached. Not enough avaliable seats!");
        }

        screening.AvaliableSeats -= ticketCount;
        _screeningRepo.Update(screening);
        
        decimal rawTotal = ticketCount * screening.TicketPrice;
    
        if (ticketCount >= 5)
        {
            rawTotal *= 0.85m; // 15% popusta
        }
        
        int finalTotalPrice = (int)Math.Round(rawTotal);

        var newBooking = new Booking
        {
            CustomerID = customerId,
            ScreeningID = screeningId,
            TicketCount = ticketCount,
            TotalPrice = (int)(ticketCount * screening.TicketPrice),
            CreatedAt = DateTime.Now,
            Status = BookingStatus.Confirmed
        };
            _bookingRepo.Add(newBooking);
            return newBooking;
    }

    public Dictionary<int, List<Booking>> GroupBookingByScreening()
    {
        return _bookingRepo.GetAll().GroupBy(b => b.ScreeningID).ToDictionary(g => g.Key, g => g.ToList());
        
    }

    public List<Booking> GetCancelledBookings()
    {
        return _bookingRepo.GetAll().Where(b => b.Status == BookingStatus.Cancelled).ToList();
        
    }
    public int GetTotalRevenue()
    {
        return _bookingRepo.GetAll().Where(b => b.Status == BookingStatus.Confirmed).Sum(b => b.TotalPrice);
    }
    public List<Customer>GetAllCustomers()=>_customerRepo.GetAll().ToList();
    public List<Screening> GetAllScreenings() => _screeningRepo.GetAll().ToList();
    public List<Booking> GetAllBookings ()=> _bookingRepo.GetAll().ToList();
    }
