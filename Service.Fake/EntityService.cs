using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Service.Interfaces;
using Bogus;

namespace Service.Fake
{
    public class EntityService<T> : IEntityService<T> where T : Entity
    {
        protected ICollection<T> Entities {get;}
        private int _lastId;

        public EntityService(Faker<T> faker, int count)
        {
            Entities = faker.Generate(count);
            _lastId = Entities.Max(x => x.Id);
        }

        public Task<T> ReadAsync(int id)
        {
            return Task.FromResult(Entities.SingleOrDefault(x => x.Id == id));
        }

        public Task<IEnumerable<T>> ReadAsync()
        {
            return Task.FromResult(Entities.AsEnumerable());
        }

        public Task<T> CreateAsync(T entity)
        {
            entity.Id = ++_lastId;
            Entities.Add(entity);
            return Task.FromResult(entity);
        }

        public async Task UpdateAsync(int id, T entitiy)
        {
            entitiy.Id = id;
            await DeleteAsync(id);
            Entities.Add(entitiy);
        }

        public Task DeleteAsync(int id)
        {
            Entities.Remove(Entities.SingleOrDefault(x => x.Id == id));
            return Task.CompletedTask;
        }
    }
}
