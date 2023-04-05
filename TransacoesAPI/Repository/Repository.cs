using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using TransacoesAPI.Data;
using TransacoesAPI.Repository.Base;

namespace TransacoesAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public Task Insert(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        public Task Update(T entity)
        {
            try
            {
                _context.Update(entity);
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }

        }

        public Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
