using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Models.EntityClass;
using Models.Models;
using Services.Repository;

namespace Services.CQRS.Command
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Sku { get; set; }

        public class CreateProductCommandHandle : IRequestHandler<CreateProductCommand, int>
        {
            private readonly FFDbContext _context;
            private readonly IUnitOfWork _unitOfWork;
            public CreateProductCommandHandle(FFDbContext context, IUnitOfWork unitOfWork)
            {
                _context = context;
                _unitOfWork = unitOfWork;
            }

            public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                var product = new Product();
                product.Name = command.Name;
                product.Sku = command.Sku;
                await _unitOfWork.ProductService.Add(product);
                await _unitOfWork.SaveChanges();
                return product.Id;
            }
        }
    }
}
