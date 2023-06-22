using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Models.EntityClass;
using Models.Models;

namespace Services.CQRS.Command
{
    public class CreateCustomerCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public class CreateCustomerCommandHandle : IRequestHandler<CreateCustomerCommand, int>
        {
            private readonly FFDbContext _context;
            public CreateCustomerCommandHandle(FFDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
            {
                var customer = new Customer();
                customer.Name = command.Name;
                customer.Phone = command.Phone;
                customer.Address = command.Address;
                customer.Email = command.Email;
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                return customer.Id;
            }
        }
    }
}
