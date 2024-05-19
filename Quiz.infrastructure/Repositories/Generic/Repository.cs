

using Quiz.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Infrastructure.Data.Repositories.Generic
{
    public abstract class Repository<TContext, T> : IRepository<T> where T : class where TContext : DbContext
    {
        protected TContext _context;

        public Repository(TContext context) => _context = context;

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public void Delete(object id)
        {
            var entity = Get(id);
            if (entity != null)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _context.Set<T>().Attach(entity);
                }
                _context.Set<T>().Remove(entity);
            }
        }

        public T Get(object id)
        {
            var x = _context.Set<T>().Find(id);
            return x;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public void Update(T entity)
        {

            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

        }

        public void InsertOrUpdate(T entity)
        {
            var entityEntry = _context.Entry(entity);

            var primaryKeyName = entityEntry.Context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties
                .Select(x => x.Name).Single();

            var primaryKeyField = entity.GetType().GetProperty(primaryKeyName);

            var t = typeof(T);
            if (primaryKeyField == null)
            {
                throw new Exception($"{t.FullName} does not have a primary key specified. Unable to exec InsertOrUpdate call.");
            }
            var keyVal = primaryKeyField.GetValue(entity);
            var dbVal = _context.Set<T>().Find(keyVal);

            if (dbVal != null)
            {
                _context.Entry(dbVal).CurrentValues.SetValues(entity);
                _context.Set<T>().Update(dbVal);
            }
            else
            {
                _context.Set<T>().Add(entity);
            }
        }

        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }

        public void Cancel()
        {
            _context.RemoveRange(_context);
        }
    }
}

