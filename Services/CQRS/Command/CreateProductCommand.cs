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
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Sku { get; set; }

        public class CreateProductCommandHandle : IRequestHandler<CreateProductCommand, int>
        {
            private readonly FFDbContext _context;
            public CreateProductCommandHandle(FFDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                var product = new Product();
                product.Name = command.Name;
                product.Sku = command.Sku;
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return product.Id;
            }
        }
    }
}
