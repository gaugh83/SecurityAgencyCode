using SecurityAgency.Repository.DbServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAgency.Repository
{
    public interface IDbRepository
    {
        int Insert<TEntity>(TEntity entity) where TEntity : class;
        int Delete<TEntity>(TEntity entity) where TEntity : class;
        int Modify<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
        List<TEntity> GetAll<TEntity>() where TEntity : class;
        
        TEntity Find<TEntity>(Expression<Func<TEntity, bool>> match) where TEntity : class;
    }
}
