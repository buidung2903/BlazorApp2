using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.EntityClass;
using Models.Models;

namespace Services.CQRS.Command
{
    public class DeleteCustomerCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteCustomerCommandHandle : IRequestHandler<DeleteCustomerCommand, int>
        {
            private readonly FFDbContext _context;
            public DeleteCustomerCommandHandle(FFDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == command.Id);
                if (customer == null) return default;
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return customer.Id;
            }
        }
    }
}
