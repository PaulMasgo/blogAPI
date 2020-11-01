using System.Threading.Tasks;
using AutoMapper;
using BlogApi.Features;
using BlogApi.Mapping.CategoryViewModel;
using BlogApi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly Categories _categories;

        public CategoryController(ICategoryRepositoryAsync categoryRepository, IMapper mapper)
        {
            _categories = new Categories(categoryRepository, mapper);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _categories.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _categories.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save(GetCategoryViewModel category)
        {
            return Ok(await _categories.Create(category));
        }
    }
}