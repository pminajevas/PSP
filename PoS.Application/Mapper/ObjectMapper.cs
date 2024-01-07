﻿using AutoMapper;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using PoS.Core.Entities;

namespace PoS.Application.Mapper
{
    public static class Mapping
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Customer, CustomerResponse>();

            //CreateMap<UserInner, Customer>();

            CreateMap<CustomerRequest, Customer>();

            //CreateMap<UserInner, User>();

            //CreateMap<User, UserInner>()
            //    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));


            CreateMap<Business, BusinessResponse>();

            CreateMap<BusinessRequest, Business>();

            CreateMap<Role, RoleResponse>();

            CreateMap<RoleRequest, Role>();

            CreateMap<Staff, StaffResponse>();

            CreateMap<StaffRequest, Staff>();

            CreateMap<DiscountRequest, Discount>();
            CreateMap<Discount, DiscountResponse>();
            CreateMap<DiscountUpdateRequest, Discount>();
            // add more


        }
    }

}
