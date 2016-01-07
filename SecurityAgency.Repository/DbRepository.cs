using SecurityAgency.Repository.DbServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAgency.Repository 
{
    public class DbRepository : IDbRepository
    {
        /// <summary>
        /// Initilize Referance of PMMTEntities
        /// </summary>
        private SecurityAgencyEntities context = null;

        /// <summary>
        /// Referance of PMMTEntities
        /// </summary>
        public DbRepository()
        {
            context = new SecurityAgencyEntities();
        }

        /// <summary>
        /// Generic method to Create records
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert<TEntity>(TEntity entity) where TEntity : class
        {
            context.Entry(entity).State = EntityState.Added;
           
            return SaveChanges();
        }

        /// <summary>
        /// Generic method to Update records
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Modify<TEntity>(TEntity entity) where TEntity : class
        {
            context.Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }

        /// <summary>
        /// Generic method to Delete records
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete<TEntity>(TEntity entity) where TEntity : class
        {
            context.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();

        }

        /// <summary>
        /// Generic method to get all records
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public List<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return context.Set<TEntity>().ToList();
        }

        

        /// <summary>
        /// Save changes to database
        /// </summary>
        public int SaveChanges()
        {
            
                return context.SaveChanges();
            
        }

        /// <summary>
        /// Method to close or release unmanaged resources
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
        }


        /// <summary>
        /// Generic method to find records by Id
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="match"></param>
        /// <returns></returns>
        public TEntity Find<TEntity>(Expression<Func<TEntity, bool>> match) where TEntity : class
        {
            //return await context.Set<TEntity>().SingleOrDefaultAsync(match);
            return context.Set<TEntity>().FirstOrDefault(match);
        }
        

        }

     
    }

