using Microsoft.EntityFrameworkCore;
using SarajevoFilmFestival.Data;

namespace SarajevoFilmFestival.Repositories;

public class Repository<T> : IRepository<T> where T: class
{
    protected readonly DbContext _context;
    public Repository(AppDbContext context)
    {
        _context = context;
       
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }

    public IEnumerable<T> GetAll()
    {
       return _context.Set<T>().ToList();
      

    }

    public T? GetById(int id)
    {
       return _context.Set<T>().Find(id);
    }
}