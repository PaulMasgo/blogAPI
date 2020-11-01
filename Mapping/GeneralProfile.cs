using AutoMapper;
using BlogApi.Entities;
using BlogApi.Mapping.CategoryViewModel;

namespace BlogApi.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Category,GetCategoryViewModel>().ReverseMap();
        }
    }
}