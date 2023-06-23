using Models.EntityClass;
using Models.Models;
using Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(FFDbContext context) : base(context)
        {

        }
    }
}
