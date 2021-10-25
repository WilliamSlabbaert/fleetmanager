using DataLayer.entities;
using DataLayer.entities.generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Overall.paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : GeneralEntities
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

        public IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> including)
        {
            var query = _table.AsQueryable();

            if (including != null)
            {
                query = including(query);
            }
            return query;
        }
        public IQueryable<T> GetAllWithPaging(Func<IQueryable<T>, IIncludableQueryable<T, object>> including, GenericParameter genericParemeters)
        {
            var query = _table
                .Skip((genericParemeters.PageNumber - 1) * genericParemeters.PageSize)
                .Take(genericParemeters.PageSize);

            if (including != null)
            {
                query = including(query);
            }
            return query;
        }

        public T GetById(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> including)
        {

            var query = _table.Where(filter);

            if (including != null)
            {
                query = including(query);
            }
            return query.FirstOrDefault();
        }
        public IQueryable<T> GetByFilter(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> including, GenericParameter genericParemeters)
        {
            var query = _table.Where(filter)
                .Skip((genericParemeters.PageNumber - 1) * genericParemeters.PageSize)
                .Take(genericParemeters.PageSize); ;

            if (including != null)
            {
                query = including(query);
            }
            return query;
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
