using CustomerApiMicroservice.Data.Repository;
using CustomerApiMicroservice.Domain.Entities;
using CustomerApiMicroservice.Messaging.Send.Sender;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApiMicroservice.Service.Commands {
    public class UpdateCustomerCommand : IRequest<Customer> {
        public Customer Customer { get; set; }
    }
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer> {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerUpdateSender _customerUpdateSender;

        public UpdateCustomerCommandHandler(
            ICustomerUpdateSender customerUpdateSender, 
            ICustomerRepository customerRepository) {
            _customerUpdateSender = customerUpdateSender;
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken) {
            var customer = await _customerRepository.UpdateAsync(request.Customer);

            _customerUpdateSender.SendCustomer(customer);

            return customer;
        }
    }
}
