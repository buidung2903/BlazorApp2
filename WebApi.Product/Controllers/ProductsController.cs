using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models.EntityClass;
using NLog;
using Services.CQRS.Command;
using Services.CQRS.Query;
using Services.Interfaces;
using Services.Repository;
using Services.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Product.Controllers
{
    [Route("~/product-api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMediator _mediator;
        private static Logger logger = LogManager.GetLogger("ProductsController");
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IProductService productService, IMediator mediator, ILogger<ProductsController> logger, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productService = productService;
            _mediator = mediator;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllProductQuery());
            _logger.LogInformation("Get Success");
            return Ok(result);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _mediator.Send(new GetProductByIdQuery { Id = id }));
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            await _productService.Notify(result);
            return Ok(result);
        }

        // PUT api/<ProductsController>/5
        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteProductCommand { Id = id }));
        }
    }
}
