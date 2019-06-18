using System.Collections.Generic;
using Dynamic.Shared.DynamicContext.Entities;
using Dynamic.Shared.DynamicContext.Notifications;

namespace Dynamic.Shared.DynamicContext.Repositories
{
    public interface IRepository<T> : INotifiable where T : Entity
    {
        T Add(T entity);

        T GetById(params object[] id);

        IEnumerable<T> GetAll();

        void Update(T entity);

        void Remove(T entity);

        void Dispose();
    }
}