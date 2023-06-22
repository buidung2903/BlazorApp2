using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.AbstractClass;
using Models.Dtos;
using Models.EntityClass;
using Models.Models;
using NLog;
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
        private static Logger logger = LogManager.GetLogger("ProductService");

        public ProductService(IMediator mediator ,FFDbContext context, ILogger<CustomerService> logger, IMapper mapper) : base(mediator) 
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Product>> GetListProducts() 
            => await _context.Products.ToListAsync();

        public async Task Notify(int productId) => _mediator.LogInformationAction(productId, "product");
    }
}
