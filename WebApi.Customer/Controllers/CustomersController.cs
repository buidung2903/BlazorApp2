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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Customer.Controllers
{
    [Route("~/customer-api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMediator _mediator;
        private readonly ILogger<CustomersController> _logger;
        private static Logger logger = LogManager.GetLogger("CustomersController");
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CustomersController(ICustomerService customerService, IMediator mediator, ILogger<CustomersController> logger, ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerService = customerService;
            _mediator = mediator;
            _logger = logger;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllCustomerQuery());
            _logger.LogInformation("get customer success");
            return Ok(result);
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _mediator.Send(new GetCustomerByIdQuery { Id = id }));
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            await _customerService.Notify(result);
            return Ok(result);
        }

        // PUT api/<CustomersController>/5
        [HttpPut]
        public async Task<IActionResult> Put(UpdateCustomerCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteCustomerCommand { Id = id }));
        }
    }
}
