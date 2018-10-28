using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private CarsCatalog ctx = null;
        private DbSet<TEntity> dbSet = null;
        public EFGenericRepository()
        {
            ctx = new CarsCatalog();
            dbSet = ctx.Set<TEntity>();
        }

        public void Create(TEntity item)
        {
            dbSet.Add(item);
            ctx.SaveChanges();
        }

        public TEntity FindById(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsNoTracking().ToList();
        }

        public void Remove(TEntity item)
        {
            ctx.Entry(item).State = EntityState.Deleted;
            ctx.SaveChanges();
        }

        public void Update(TEntity item)
        {
            //var trackedEntity = context.MyObjectsPropertys.Find(updatedObject.Id);
            //context.Entry(trackedEntity).CurrentValues.SetValues(updatedObject);
            //context.Entry(trackedEntity).State = EntityState.Modified;
            //context.SaveChanges();
            object trackedEntity = null;

            if (item is Brand)
            {
                trackedEntity = ctx.Brands.Find((item as Brand).Id);
            }
            else if (item is Model)
            {
                trackedEntity = ctx.Models.Find((item as Model).Id);
            }
            else if (item is Car)
            {
                trackedEntity = ctx.Cars.Find((item as Car).Id);
            }
            else if (item is Cost)
            {
                trackedEntity = ctx.Prices.Find((item as Cost).Id);
            }

            //var trackedEntity = ctx.Brands.Find((item as Brand).Id);
            ctx.Entry(trackedEntity).CurrentValues.SetValues(item);

            ctx.Entry(trackedEntity).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
