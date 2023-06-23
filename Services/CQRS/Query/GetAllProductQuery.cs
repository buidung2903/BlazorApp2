using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.EntityClass;
using Models.Models;
using Services.Repository;
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
            private readonly IUnitOfWork _unitOfWork;
            public GetAllProductQueryHandler(FFDbContext context, IUnitOfWork unitOfWork)
            {
                _context = context;
                _unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<Product>> Handle(GetAllProductQuery query, CancellationToken cancellationToken)
            {
                var productList = await _unitOfWork.ProductService.GetAll();
                return productList;
            }
        }
    }
}
