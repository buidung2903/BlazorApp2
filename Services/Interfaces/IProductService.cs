﻿using Models.Dtos;
using Models.EntityClass;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetListProducts();
        Task Notify(int productId);
    }
}
