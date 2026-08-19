using InventoryMS.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Services.IServiceModels
{
    public interface IServices<T> where T : class
    {
        Task<List<T>> GetAllAsync(GenericRequest<T> request);
        Task<T> GetAsync(GenericRequest<T> request);
        Task AddAsync(T entity, CancellationToken cancellationToken);
        void Remove(T entity);
        void RemoveRange(List<T> entities);
    }
}
