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
    public class DeleteProductCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteProductCommandHandle : IRequestHandler<DeleteProductCommand, int>
        {
            private readonly FFDbContext _context;
            public DeleteProductCommandHandle(FFDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == command.Id);
                if (product == null) return default;
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return product.Id;
            }
        }
    }
}
