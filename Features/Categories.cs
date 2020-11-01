using AutoMapper;
using System.Collections.Generic;
using BlogApi.Repository.Interfaces;
using BlogApi.Mapping.CategoryViewModel;
using BlogApi.Entities;
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

        public async Task<Response<GetCategoryViewModel>> GetById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            var result = _mapper.Map<GetCategoryViewModel>(category);
            return new Response<GetCategoryViewModel>(result);
        }

        public async Task<Response<GetCategoryViewModel>> Create(GetCategoryViewModel categoryViewModel)
        {
            var category = _mapper.Map<Category>(categoryViewModel);
            await _categoryRepository.AddAsync(category);
            return new Response<GetCategoryViewModel>(categoryViewModel);
        }
    }
}