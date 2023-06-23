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
    public class UpdateCustomerCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public class UpdateCustomerCommandHandle : IRequestHandler<UpdateCustomerCommand, int>
        {
            private readonly FFDbContext _context;
            private readonly IUnitOfWork _unitOfWork;
            public UpdateCustomerCommandHandle(FFDbContext context, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _context = context;
            }

            public async Task<int> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
            {
                var customer = await _unitOfWork.CustomerService.GetById(command.Id);
                if (customer == null) return default;
                customer.Name = command.Name;
                customer.Phone = command.Phone;
                customer.Address = command.Address;
                customer.Email = command.Email;
                _unitOfWork.CustomerService.Update(customer);
                await _unitOfWork.SaveChanges();
                return customer.Id;
            }
        }
    }
}
