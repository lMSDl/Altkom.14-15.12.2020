using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Service.Interfaces
{
    public interface IEntityService<T> where T : Entity
    {
        Task<T> ReadAsync(int id);
        Task<IEnumerable<T>> ReadAsync();
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(int id, T entitiy);
        Task DeleteAsync(int id);
    }
}
