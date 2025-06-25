using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LogTagSensor.Domain.IRepositories
{
    public interface IGenericRepository<T> where T: class
    {
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
        Task Add(T entity);
        Task<T> Update(T entity);
    }
}
