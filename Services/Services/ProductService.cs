using AutoMapper;
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
    public class ProductService : Colleague, IProductService
    {
        private readonly FFDbContext _context;
        private readonly ILogger<CustomerService> _logger;
        private readonly IMapper _mapper;

        public ProductService(IMediator mediator ,FFDbContext context, ILogger<CustomerService> logger, IMapper mapper) : base(mediator) 
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Product>> GetListProducts() 
            => await _context.Products.ToListAsync();

        public async Task<bool> CreateProduct(ProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                _mediator.LogInformationAction(product, "product");
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
