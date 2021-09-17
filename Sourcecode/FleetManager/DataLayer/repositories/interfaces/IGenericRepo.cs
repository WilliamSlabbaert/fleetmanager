using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.repositories
{
    public interface IGenericRepo<T> where T : class
    {
        IQueryable<T> GetAll(string[] includes);
        T GetById(int id);
        void AddEntity(T obj);
        void UpdateEntity(T obj);
        void DeleteEntity(int id);
        void Save();
    }
}
