using AutoMapper;
using Homework_4.Models;
using Homework_4.Models.DTO;

namespace Homework_4.Repo
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
