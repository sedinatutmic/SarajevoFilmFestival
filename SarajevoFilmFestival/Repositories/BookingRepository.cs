using SarajevoFilmFestival.Data;
using SarajevoFilmFestival.Models;

namespace SarajevoFilmFestival.Repositories;

public class BookingRepository : Repository<Booking>
{
    public BookingRepository(AppDbContext context) : base(context)
    {
        
    }
    
}