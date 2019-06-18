using System;
using System.Collections.Generic;
using System.Linq;
using Dynamic.Infra.DynamicContext.DataContext;
using Dynamic.Shared.DynamicContext.Entities;
using Dynamic.Shared.DynamicContext.Repositories;
using FluentValidator;
using Microsoft.EntityFrameworkCore;

namespace Dynamic.Infra.DynamicContext.Repositories
{
    public class Repository<T> : Notifiable, IRepository<T> where T : Entity
    {
        public readonly DynamicDataContext context;

        public Repository(DynamicDataContext context) => this.context = context;

        public T Add(T entity)
        {
            try
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                AddNotification("Error", e.Message);
            }

            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> result = new List<T>();

            try
            {
                result = context.Set<T>().ToList();
            }
            catch (Exception e)
            {
                AddNotification("Error", e.Message);
            }

            return result;
        }

        public T GetById(params object[] id)
        {
            T entity = null;

            try
            {
                entity = context.Set<T>().Find(id);
            }
            catch (Exception e)
            {
                AddNotification("Error", e.Message);
            }

            return entity;
        }

        public void Remove(T entity)
        {
            try
            {
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                AddNotification("Error", e.Message);
            }
        }

        public void Update(T entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                AddNotification("Error", e.Message);
            }
        }

        public void Dispose() => context.Dispose();
    }
}