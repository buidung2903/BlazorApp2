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
    public class GetAllCustomerQuery : IRequest<IEnumerable<Customer>>
    {
        public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, IEnumerable<Customer>>
        {
            private readonly FFDbContext _context;
            public GetAllCustomerQueryHandler(FFDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Customer>> Handle(GetAllCustomerQuery query, CancellationToken cancellationToken)
            {
                var customerList = await _context.Customers.ToListAsync();
                return customerList;
            }
        }
    }
}
