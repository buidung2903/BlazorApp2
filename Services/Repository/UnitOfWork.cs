using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FFDbContext _context;
        public IProductRepository ProductService { get; }
        public ICustomerRepository CustomerService { get; }
        public UnitOfWork(FFDbContext context, IProductRepository productRepository, ICustomerRepository customerRepository)
        {
            _context = context;
            ProductService = productRepository;
            CustomerService = customerRepository;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
