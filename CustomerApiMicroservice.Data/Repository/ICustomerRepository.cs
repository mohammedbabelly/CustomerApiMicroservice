using CustomerApiMicroservice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApiMicroservice.Data.Repository {
    public interface ICustomerRepository : IRepository<Customer> {
        Task<Customer> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
