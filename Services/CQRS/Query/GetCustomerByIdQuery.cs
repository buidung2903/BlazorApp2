using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.EntityClass;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CQRS.Query
{
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public int Id { get; set; }

        public class GetCustomerByIdQueryHandle : IRequestHandler<GetCustomerByIdQuery, Customer>
        {
            private readonly FFDbContext _context;
            public GetCustomerByIdQueryHandle(FFDbContext context)
            { 
                _context = context;
            }

            public async Task<Customer> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == query.Id);
                return customer;
            }
        }
    }
}
