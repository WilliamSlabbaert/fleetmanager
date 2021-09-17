using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private FleetManagerContext _context = null;
        private DbSet<T> _table = null;

        public GenericRepo(FleetManagerContext context)
        {
            this._context = context;
            this._table = context.Set<T>();
        }

        public void AddEntity(T obj)
        {
            _table.Add(obj);
        }

        public void DeleteEntity(int id)
        {
            T existing = _table.Find(id);
            _table.Remove(existing);
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] including)
        {
            var query = _table.AsQueryable();
            if (including != null)
                including.ToList().ForEach(include =>
                {
                    if (include != null)
                        query = query.Include(include);
                });
            return query;
        }

        public T GetById(int id)
        {
            return _table.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateEntity(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
