using CustomerApiMicroservice.Data.Database;
using CustomerApiMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApiMicroservice.Data.Repository {
    public class CustomerRepository : Repository<Customer>, ICustomerRepository {
        public CustomerRepository(CustomerContext customerContext) : base(customerContext) {
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken) {
            return await CustomerContext.Customer.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
