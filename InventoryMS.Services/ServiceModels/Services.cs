using InventoryMS.Database.Data;
using InventoryMS.Models.Request;
using InventoryMS.Services.IServiceModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Services.ServiceModels
{
    public class Services<T>(InventoryMSDbContext dbcontext) : IServices<T> where T : class
    {
        private readonly DbSet<T> dbSet = dbcontext.Set<T>();

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<List<T>> GetAllAsync(GenericRequest<T> request)
        {
            IQueryable<T> query = request.NoTracking == true ? dbSet.AsNoTracking() : dbSet;
            if(request.Expression != null)
            {
                query = query.Where(request.Expression);
            }
            if(request.IncludeProperties != null)
            {
                foreach(var property in request.IncludeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return await query.ToListAsync(request.CancellationToken);
        }

        public async Task<T> GetAsync(GenericRequest<T> request)
        {
            IQueryable<T> query = request.NoTracking == true ? dbSet.AsNoTracking() : dbSet;
            if (request.Expression != null)
            {
                query = query.Where(request.Expression);
            }
            if (request.IncludeProperties != null)
            {
                foreach (var property in request.IncludeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return await query.FirstOrDefaultAsync(request.CancellationToken);
            
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(List<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
