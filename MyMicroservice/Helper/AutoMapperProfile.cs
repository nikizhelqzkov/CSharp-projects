using AutoMapper;
using MyMicroservice.DTOModels;
using MyMicroservice.Models;

namespace MyMicroservice.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OrderDTO, Order>()
                .ForMember(
                            dest => dest.OrderId,
                            opt => opt.MapFrom(src => src.Id))
                .ForMember(
                            dest => dest.OrderDate,
                            opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(
                            dest => dest.OrderStatus,
                            opt => opt.MapFrom(src => 4))

                //.ForMember(
                //            dest => dest.Store.StoreName,
                //            opt => opt.MapFrom(src => src.StoreName))
                //.ForMember(
                //            dest => dest.Customer.FirstName,
                //            opt => opt.MapFrom(src => src.CustomerFirstName))
                //.ForMember(
                //            dest => dest.Customer.LastName,
                //            opt => opt.MapFrom(src => src.CustomerLastName))
                //.ForMember(
                //            dest => dest.Customer.Email,
                //            opt => opt.MapFrom(src => src.CustomerEmail))
                //.ForMember(
                //            dest => dest.Staff.FirstName,
                //            opt => opt.MapFrom(src => src.StaffFirstName))
                //.ForMember(
                //            dest => dest.Staff.LastName,
                //            opt => opt.MapFrom(src => src.StaffLastName))
                .ForMember(
                            dest => dest.OrderItems,
                            opt => opt.MapFrom(src => src.OrderItems))
                .ReverseMap();



            CreateMap<OrderItemsDTO, OrderItem>()
                //.ForMember(
                //          dest => dest.Product.ProductName,
                //          opt => opt.MapFrom(src => src.ProductName))
                .ReverseMap();
        }
    }
}
