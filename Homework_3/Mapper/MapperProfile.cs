using AutoMapper;
using Homework_3.Models;
using Homework_3.Models.DTO;

namespace Homework_3.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<ProductEntity, ProductDTO>(MemberList.Destination).ReverseMap();
            CreateMap<StorageEntity, StorageDTO>(MemberList.Destination).ReverseMap();
            CreateMap<CategoryEntity, CategoryDTO>(MemberList.Destination).ReverseMap();
        }
    }
}
