using AutoMapper;
using System.Collections.Generic;
using BlogApi.Repository.Interfaces;
using BlogApi.Mapping.Category;
using System.Threading.Tasks;

namespace BlogApi.Features
{
    public class Categories
    {
        private readonly ICategoryRepositoryAsync _categoryRepository;
        private readonly IMapper _mapper;

        public Categories(ICategoryRepositoryAsync categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetCategoryViewModel>>> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<GetCategoryViewModel>>(categories);
            return new Response<IEnumerable<GetCategoryViewModel>>(result);
        }
    }
}