using AutoMapper;
using BlogApi.Features;
using BlogApi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly Categories _categories;

        public CategoryController(ICategoryRepositoryAsync categoryRepository, IMapper mapper)
        {
            _categories = new Categories(categoryRepository, mapper);
        }
    }
}