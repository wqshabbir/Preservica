using Backend.Models;
using Backend.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IValidator<Customer> _validator;
        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService, IValidator<Customer> validator)
        {
            _logger = logger;
            _customerService = customerService;
            _validator = validator;
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerService.GetCustomersAsync();
            return new OkObjectResult(customers);
        }

        [Route("{customerId}")]
        [HttpGet]
        public async Task<IActionResult> GetCustomer(string customerId)
        {
            var customer = await _customerService.GetCustomerAsync(customerId);

            if (customer == null)
            {
                return new NotFoundObjectResult($"Customer with Id:{customerId} not found");
            }
            return new OkObjectResult(customer);
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer(Customer customer)
        {
            var validationResult = _validator.Validate(customer);

            if (validationResult.IsValid)
            {
                var customerId = await _customerService.CreateCustomerAsync(customer);
                var location = new Uri($"{Request.Scheme}://{Request.Host.Value}{Request.Path.Value}/{customerId}");
                return new CreatedResult(location, customer);
            }
            else
            {
                _logger.LogError("Invalid customer create request received.");
                return new BadRequestObjectResult($"Failed to validate customer create request. Reason:{validationResult.Errors}");
            }
        }

        [Route("{customerId}")]
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(string customerId, Customer customer)
        {
            var validationResult = _validator.Validate(customer);
            if (validationResult.IsValid)
            {
                var result = await _customerService.UpdateCustomerAsync(customerId, customer);
                return new OkObjectResult(result);
            }
            else
            {
                _logger.LogError("Invalid customer UpdateCustomer request received.");
                return new BadRequestObjectResult($"Failed to validate customer UpdateCustomer request. Reason:{validationResult.Errors}");
            }
        }

        [Route("{customerId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(string customerId)
        {
            var result = await _customerService.DeleteCustomerAsync(customerId);
            return result ? new OkObjectResult("Customer Deleted") : new NotFoundObjectResult($"Customer with Id:{customerId} not found");
        }
    }
}
