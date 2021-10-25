using DataLayer.entities;
using DataLayer.entities.generic;
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
    public interface IGenericRepo<T> where T : GeneralEntities
    {
        IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> including);
        public IQueryable<T> GetAllWithPaging(Func<IQueryable<T>, IIncludableQueryable<T, object>> including, GenericParameter genericParemeters);
        public T GetById(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> including);
        public IQueryable<T> GetByFilter(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> including, GenericParameter genericParemeters);
        void AddEntity(T obj);
        void UpdateEntity(T obj);
        void DeleteEntity(int id);
        void Save();
    }
}
