using CustomerApiMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace CustomerApiMicroservice.Data.Database {
    public class CustomerContext : DbContext {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) {
            var customers = new[]
            {
                new Customer
        {
            Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
            FirstName = "Wolfgang",
            LastName = "Ofner",
            Birthday = new DateTime(1989, 11, 23),
            Age = 30
        },
        new Customer
        {
            Id = Guid.Parse("654b7573-9501-436a-ad36-94c5696ac28f"),
            FirstName = "Darth",
            LastName = "Vader",
            Birthday = new DateTime(1977, 05, 25),
            Age = 43
        },
        new Customer
        {
            Id = Guid.Parse("971316e1-4966-4426-b1ea-a36c9dde1066"),
            FirstName = "Son",
            LastName = "Goku",
            Birthday = new DateTime(1937, 04, 16),
            Age = 83
        }
    };

            Customer.AddRange(customers);
            SaveChanges();
        }

        public DbSet<Customer> Customer { get; set; }
    }
}
