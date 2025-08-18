using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Educational_Courses_Platform.DataAccess.Data;
using Educational_Courses_Platform.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Courses_Platform.DataAccess.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predict=null, string? includeword= null)
        {
            IQueryable<T> query = _dbSet;
            if(predict != null)
            {
                query = query.Where(predict);
            }
            if(includeword != null)
            {
                foreach(var item in includeword.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query=query.Include(item);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>>? predict = null, string? includeword = null)
        {
            IQueryable<T> query = _dbSet;
            if (predict != null)
            {
                query = query.Where(predict);
            }
            if (includeword != null)
            {
                foreach (var item in includeword.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.SingleOrDefault();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
           _dbSet.RemoveRange(entities);
        }
    }
}
