using ApiDemo.Dto;
using ApiDemo.Models;
using AutoMapper;

namespace ApiDemo.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDTO>();    
        }
    }
}
