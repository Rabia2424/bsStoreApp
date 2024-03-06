using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        protected readonly RepositoryContext _context;

        protected RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public void Create(T t) => _context.Set<T>().Add(t);

        public void Delete(T t) => _context.Set<T>().Remove(t);


        public IQueryable<T> FindAll(bool trackChanges) =>
            trackChanges ?
            _context.Set<T>() : 
            _context.Set<T>().AsNoTracking();


        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
            bool trackChanges)=>
            trackChanges ?
            _context.Set<T>().Where(expression) :
            _context.Set<T>().Where(expression).AsNoTracking();
       

        public void Update(T t) => _context.Set<T>().Update(t);

    }
}
