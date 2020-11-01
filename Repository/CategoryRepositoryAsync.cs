using BlogApi.Context;
using BlogApi.Entities;
using BlogApi.Repository.Interfaces;

namespace BlogApi.Repository
{
    public class CategoryRepositoryAsync : GenericRepositoryAsync<Category>, ICategoryRepositoryAsync
    {
        public CategoryRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}