using LogTagSensor.Domain.IRepositories;
using LogTagSensor.Infrastructure.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LogTagSensor.Infrastructure.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DeviceMonitoringContext _context;
        public DbSet<T> dbSet { get; set; }    
        public GenericRepository(DeviceMonitoringContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Context cannot be null");
            dbSet = _context.Set<T>();
        }

        public IQueryable<T> GetQueryable() => dbSet.AsNoTracking();

        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
           
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = GetQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
