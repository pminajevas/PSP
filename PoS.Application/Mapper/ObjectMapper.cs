using AutoMapper;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using PoS.Core.Entities;

namespace PoS.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BusinessRequest, Business>();
            CreateMap<Business,  BusinessResponse>();

            CreateMap<CustomerRequest, Customer>();
            CreateMap<Customer, CustomerResponse>();

            CreateMap<DiscountRequest, Discount>();
            CreateMap<Discount, DiscountResponse>();

            CreateMap<RoleRequest, Role>();
            CreateMap<Role, RoleResponse>();

            CreateMap<StaffRequest, Staff>();
            CreateMap<Staff, StaffResponse>();

            CreateMap<LoyaltyProgramRequest, LoyaltyProgram>();
            CreateMap<LoyaltyProgram, LoyaltyProgramResponse>();

            CreateMap<CouponRequest, Coupon>();
            CreateMap<Coupon, CouponResponse>();

            CreateMap<TaxRequest, Tax>();
            CreateMap<Tax, TaxResponse>();

            CreateMap<OrderRequest, Order>();
            CreateMap<Order, OrderResponse>();

            CreateMap<OrderItemRequest, OrderItem>();
            CreateMap<OrderItem, OrderItemResponse>();

            CreateMap<AppointmentRequest, Appointment>();
            CreateMap<Appointment, AppointmentResponse>();

            CreateMap<PaymentRequest, Payment>();
            CreateMap<PaymentMethodRequest, PaymentMethod>();
            
            CreateMap<ItemRequest, Item>();
            CreateMap<Item, ItemResponse>();

            CreateMap<ServiceRequest, Service>();
            CreateMap<Service, ServiceResponse>();
        }
    }

}
