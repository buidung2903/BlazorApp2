using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Sku { get; set; }
    }
}
