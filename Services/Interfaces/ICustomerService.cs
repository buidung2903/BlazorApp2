using Models.Dtos;
using Models.EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetListCustomers();
        Task<bool> CreateCustomer(CustomerDto customerDto);
    }
}
