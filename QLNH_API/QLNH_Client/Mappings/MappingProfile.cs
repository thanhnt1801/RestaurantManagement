using AutoMapper;
using QLNH_Client.DTOs;
using QLNH_Client.Models;

namespace QLNH_Client.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
            CreateMap<Role, RoleDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Restaurant, RestaurantDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Item, ItemDTO>().ReverseMap();
            CreateMap<ItemImage, ItemImageDTO>().ReverseMap();
            CreateMap<Guest, GuestDTO>().ReverseMap();
            CreateMap<GuestTable, GuestTableDTO>().ReverseMap();
            CreateMap<Location, LocationDTO>().ReverseMap();
            CreateMap<Price, PriceDTO>().ReverseMap();
            CreateMap<Size, SizeDTO>().ReverseMap();
            CreateMap<Status, StatusDTO>().ReverseMap();
            CreateMap<Unit, UnitDTO>().ReverseMap();


        }
    }
}
