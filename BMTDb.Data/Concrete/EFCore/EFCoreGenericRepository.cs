#pragma warning disable CS8603

using BMTDb.Data.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Data.Concrete.EFCore
{
    public class EFCoreGenericRepository<TEntity> : IRepository<TEntity>
        where TEntity  : class
    {
        protected readonly DbContext context;
        public EFCoreGenericRepository(DbContext context)
        {
            this.context = context;
        }

        public List<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }
        public void Create(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
