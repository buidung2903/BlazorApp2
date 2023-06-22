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
    public class UpdateProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }

        public class UpdateProductCommandHandle : IRequestHandler<UpdateProductCommand, int>
        {
            private readonly FFDbContext _context;
            public UpdateProductCommandHandle(FFDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == command.Id);
                if (product == null) return default;
                product.Name = command.Name;
                product.Sku = command.Sku;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return product.Id;
            }
        }
    }
}
