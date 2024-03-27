using AutoMapper;
using Homework_4._1.Models;
using Homework_4._1.Models.DTO;

namespace Homework_4._1.Mapper
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
