using System.Threading;
using System.Threading.Tasks;
using CustomerApiMicroservice.Data.Repository;
using CustomerApiMicroservice.Domain.Entities;
using MediatR;

namespace CustomerApiMicroservice.Service.Querires {
    public class GetCustomerQuery : IRequest<Customer> {
        public string Id { get; set; }
    }
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Customer> {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerQueryHandler(ICustomerRepository customerRepository) {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(GetCustomerQuery request, CancellationToken cancellationToken) {
            return await _customerRepository.GetCustomerByIdAsync(System.Guid.Parse(request.Id), cancellationToken);
        }
    }
}