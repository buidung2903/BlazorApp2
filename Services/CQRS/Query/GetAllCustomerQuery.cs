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
    public class GetAllCustomerQuery : IRequest<IEnumerable<Customer>>
    {
        public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, IEnumerable<Customer>>
        {
            private readonly FFDbContext _context;
            private readonly IUnitOfWork _unitOfWork;
            public GetAllCustomerQueryHandler(FFDbContext context, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _context = context;
            }
            public async Task<IEnumerable<Customer>> Handle(GetAllCustomerQuery query, CancellationToken cancellationToken)
            {
                var customerList = await _unitOfWork.CustomerService.GetAll();
                return customerList;
            }
        }
    }
}
