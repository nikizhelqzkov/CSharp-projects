using AutoMapper;
using MyMicroservice.DTOModels;
using MyMicroservice.Models;

namespace MyMicroservice.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Store, StoreDTO>().ReverseMap();

            CreateMap<OrderDTO, Order>()
                .ForMember(
                            dest => dest.OrderId,
                            opt => opt.MapFrom(src => src.Id))
                .ForPath(
                            dest => dest.Store.StoreName,
                            opt => opt.MapFrom(src => src.StoreName))

                .ForPath(
                            dest => dest.Customer.FirstName,
                            opt => opt.MapFrom(src => src.CustomerFirstName))
                .ForPath(
                            dest => dest.Customer.LastName,
                            opt => opt.MapFrom(src => src.CustomerLastName))
                .ForPath(
                            dest => dest.Customer.Email,
                            opt => opt.MapFrom(src => src.CustomerEmail))
                .ForPath(
                            dest => dest.Staff.FirstName,
                            opt => opt.MapFrom(src => src.StaffFirstName))
                .ForPath(
                            dest => dest.Staff.LastName,
                            opt => opt.MapFrom(src => src.StaffLastName))
                .ForPath(
                            dest => dest.OrderItems,
                            opt => opt.MapFrom(src => src.OrderItems))
                .ReverseMap();



            CreateMap<OrderItemsDTO, OrderItem>()
                .ForPath(
                          dest => dest.Product.ProductName,
                          opt => opt.MapFrom(src => src.ProductName))
                .ReverseMap();

            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();


        }
    }
}
