using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using System.Linq.Expressions;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly MusicStoreDbContext _context;

    public Repository(MusicStoreDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity)
    {
        try
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error adding entity", ex);
        }
    }

    public async Task UpdateAsync(T entity)
    {
        try
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating entity", ex);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error deleting entity", ex);
        }
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        try
        {
            return await _context.Set<T>().FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving entity", ex);
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        try
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving all entities", ex);
        }
    }

    public IQueryable<T> Query()
    {
        return _context.Set<T>().AsQueryable();
    }

   

}
