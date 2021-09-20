using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.repositories
{
    public interface IGenericRepo<T> where T : class
    {
        IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> including);
        public T GetById(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> including);
        void AddEntity(T obj);
        void UpdateEntity(T obj);
        void DeleteEntity(int id);
        void Save();
    }
}
