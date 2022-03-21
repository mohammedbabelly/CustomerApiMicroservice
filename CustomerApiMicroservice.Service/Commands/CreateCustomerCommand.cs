using CustomerApiMicroservice.Data.Database;
using CustomerApiMicroservice.Data.Repository;
using CustomerApiMicroservice.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApiMicroservice.Service.Commands {
    public class CreateCustomerCommand : IRequest<Customer> {
        public Customer Customer { get; set; }
    }
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer> {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository) {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken) {
            return await _customerRepository.AddAsync(request.Customer);
        }
    }
}
