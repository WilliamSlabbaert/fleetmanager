using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private FleetManagerContext context = null;
        private DbSet<T> table = null;

        public GenericRepo(FleetManagerContext context)
        {
            this.context = context;
            this.table = context.Set<T>();
        }

        public void AddEntity(T obj)
        {
            table.Add(obj);
        }

        public void DeleteEntity(int id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public IQueryable<T> GetAll()
        {
            return table;
        }

        public T GetById(int id)
        {
            return table.Find(id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateEntity(T obj)
        {
            table.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
        }
    }
}
