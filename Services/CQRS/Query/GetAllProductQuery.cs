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
    public class GetAllProductQuery : IRequest<IEnumerable<Product>>
    {
        public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IEnumerable<Product>>
        {
            private readonly FFDbContext _context;
            public GetAllProductQueryHandler(FFDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Product>> Handle(GetAllProductQuery query, CancellationToken cancellationToken)
            {
                var productList = await _context.Products.ToListAsync();
                return productList;
            }
        }
    }
}
