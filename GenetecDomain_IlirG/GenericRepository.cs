using Dapper;
using GenetecDomain_IlirG.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenetecDomain_IlirG
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        protected Genetec_IlirGContext context;
        internal DbSet<TEntity> dbSet;



        public IDbConnection Connection
        {
            get
            {
                var conn = context.Database.GetDbConnection();

                if (conn.State != ConnectionState.Open)
                    conn.Open();
                return conn;
            }
        }



        public GenericRepository(Genetec_IlirGContext context)
        {
            this.context = context;// new Genetec_IlirGContext();
            this.dbSet = context.Set<TEntity>();
        }

        protected virtual async Task<IEnumerable<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        protected virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        protected virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
            return entity;
        }
         protected virtual async Task<IEnumerable<TEntity>> InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
            await SaveAsync();
            return entities;
        }

        protected virtual async Task<int> DeleteAsync(object id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            Delete(entityToDelete);
            return await context.SaveChangesAsync();
        }
        protected virtual async Task<int> UpdateAsync(TEntity entityToUpdate)
        {

            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            return await context.SaveChangesAsync();
        }

        void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);

        }
        async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

    }

}
