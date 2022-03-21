using CustomerApiMicroservice.Domain.Entities;

namespace CustomerApiMicroservice.Messaging.Send.Sender {
    public interface ICustomerUpdateSender {
        void SendCustomer(Customer customer);
    }
}
