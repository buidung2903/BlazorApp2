using AutoMapper;
using Models.Dtos;
using Models.EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CustomerDto, Customer>();
            CreateMap<ProductDto, Product>();
        }
    }
}
