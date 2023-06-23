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
using Services.Repository;

namespace Services.CQRS.Command
{
    public class DeleteProductCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteProductCommandHandle : IRequestHandler<DeleteProductCommand, int>
        {
            private readonly FFDbContext _context;
            private readonly IUnitOfWork _unitOfWork;
            public DeleteProductCommandHandle(FFDbContext context, IUnitOfWork unitOfWork)
            {
                _context = context;
                _unitOfWork = unitOfWork;
            }

            public async Task<int> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
            {
                var product = await _unitOfWork.ProductService.GetById(command.Id);
                if (product == null) return default;
                _unitOfWork.ProductService.Delete(product);
                await _unitOfWork.SaveChanges();
                return product.Id;
            }
        }
    }
}
