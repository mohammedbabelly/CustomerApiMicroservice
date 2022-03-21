using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CustomerApiMicroservice.Data.Repository;
using CustomerApiMicroservice.Domain.Entities;
using MediatR;

namespace CustomerApiMicroservice.Service.Querires {
    public class GetAllCustomersQuery : IRequest<List<Customer>> {
    }
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<Customer>> {
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomersQueryHandler(ICustomerRepository customerRepository) {
            _customerRepository = customerRepository;
        }

        public async Task<List<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken) {
            return _customerRepository.GetAll().ToList();
        }
    }
}