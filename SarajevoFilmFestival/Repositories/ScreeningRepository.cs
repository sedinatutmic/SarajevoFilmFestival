using SarajevoFilmFestival.Data;
using SarajevoFilmFestival.Models;

namespace SarajevoFilmFestival.Repositories;

public class ScreeningRepository :Repository<Screening>
{
    public ScreeningRepository(AppDbContext context) : base(context)
    {
        
    }
}