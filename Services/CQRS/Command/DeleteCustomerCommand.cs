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
    public class DeleteCustomerCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteCustomerCommandHandle : IRequestHandler<DeleteCustomerCommand, int>
        {
            private readonly FFDbContext _context;
            private readonly IUnitOfWork _unitOfWork;
            public DeleteCustomerCommandHandle(FFDbContext context, IUnitOfWork unitOfWork)
            {
                _context = context;
                _unitOfWork = unitOfWork;
            }

            public async Task<int> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
            {
                var customer = await _unitOfWork.CustomerService.GetById(command.Id);
                if (customer == null) return default;
                _unitOfWork.CustomerService.Delete(customer);
                await _unitOfWork.SaveChanges();
                return customer.Id;
            }
        }
    }
}
