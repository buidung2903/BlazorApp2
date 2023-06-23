using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductService { get; }
        ICustomerRepository CustomerService { get; }
        Task<int> SaveChanges();
    }
}
