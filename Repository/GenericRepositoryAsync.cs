using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using BlogApi.Context;
using BlogApi.Entities;

namespace BlogApi.Repository
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : BaseEntity
    {

        private readonly DbSet<T> _entity;
        private readonly ApplicationDbContext _dbContext;

        public GenericRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _entity = _dbContext.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _entity.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _entity.AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entity.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}