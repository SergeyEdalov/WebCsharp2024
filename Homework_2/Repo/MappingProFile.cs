using AutoMapper;
using Homework_2.Models;
using Homework_2.Models.DTO;

namespace Homework_2.Repo
{
    public class MappingProFile :Profile
    {
        public MappingProFile() 
        {
            CreateMap<Product, ProductDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Category, CategoryDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Storage, StorageDTO>(MemberList.Destination).ReverseMap();
        }
    }
}
