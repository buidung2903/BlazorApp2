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
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandle : IRequestHandler<GetProductByIdQuery, Product>
        {
            private readonly FFDbContext _context;
            public GetProductByIdQueryHandle(FFDbContext context)
            { 
                _context = context;
            }

            public async Task<Product> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == query.Id);
                return product;
            }
        }
    }
}
