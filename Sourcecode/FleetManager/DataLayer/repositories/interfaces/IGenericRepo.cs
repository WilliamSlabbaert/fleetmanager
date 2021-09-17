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
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] including);
        T GetById(int id);
        void AddEntity(T obj);
        void UpdateEntity(T obj);
        void DeleteEntity(int id);
        void Save();
    }
}
