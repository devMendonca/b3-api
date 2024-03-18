using b3_data;
using b3_data.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace b3_data.Repositorio
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected Contexto _db;

        public Repository(Contexto db)
        {
            _db = db;
        }

        public IQueryable<T> Get()
        {
            return _db.Set<T>().AsNoTracking();
        }

        public async Task<T> GetById(Expression<Func<T, bool>> predicate)
        {
            return await _db.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }
        public void Update(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.Set<T>().Update(entity);
        }
    }
}
