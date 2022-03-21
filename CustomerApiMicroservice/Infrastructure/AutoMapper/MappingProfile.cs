using AutoMapper;
using CustomerApiMicroservice.Domain.Entities;
using CustomerApiMicroservice.Models;

namespace CustomerApiMicroservice.Infrastructure.AutoMapper {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<CreateCustomerModel, Customer>().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<UpdateCustomerModel, Customer>();
        }
    }
}
