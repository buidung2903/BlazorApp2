using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Dtos;
using Models.EntityClass;
using Models.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ProductService : IProductService
    {
        private readonly FFDbContext _context;
        private readonly ILogger<CustomerService> _logger;
        private readonly IMapper _mapper;

        public ProductService(FFDbContext context, ILogger<CustomerService> logger, IMapper mapper) 
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
                var customer = _mapper.Map<Product>(productDto);
                await _context.Products.AddAsync(customer);
                await _context.SaveChangesAsync();
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
