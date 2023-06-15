﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.AbstractClass;
using Models.Dtos;
using Models.EntityClass;
using Models.Models;
using Services.Interfaces;
using Services.MediatorPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CustomerService : Colleague, ICustomerService
    {
        private readonly FFDbContext _context;
        private readonly ILogger<CustomerService> _logger;
        private readonly IMapper _mapper;

        public CustomerService(IMediator mediator, FFDbContext context, ILogger<CustomerService> logger, IMapper mapper) : base(mediator)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Customer>> GetListCustomers() 
            => await _context.Customers.ToListAsync();

        public async Task<bool> CreateCustomer(CustomerDto customerDto)
        {
            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                _mediator.LogInformationAction(customer, "customer");
                return true;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
