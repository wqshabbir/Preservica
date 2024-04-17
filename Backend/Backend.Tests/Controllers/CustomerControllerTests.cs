using Backend.Controllers;
using Backend.Models;
using Backend.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Backend.Tests.Controllers
{

    public class CustomerControllerTests
    {
        private readonly Mock<ILogger<CustomerController>> _loggerMock;
        private readonly Mock<IValidator<Customer>> _validatorMock;
        private readonly Mock<ICustomerService> _customerServiceMock;

        private Customer _customer;
        private ValidationResult _validationResult;
        private CustomerController _customerController;

        public CustomerControllerTests()
        {
            _loggerMock = new Mock<ILogger<CustomerController>>();
            _customerServiceMock = new Mock<ICustomerService>();
            _validatorMock = new Mock<IValidator<Customer>>();

            _customer = new Customer() { Id = Guid.NewGuid().ToString(), Name = "Waqas", Email = "wqshabbir@gmail.com" };
            _validationResult = new ValidationResult();
            _validatorMock.Setup(s => s.Validate(It.IsAny<Customer>()))
                .Returns(() => _validationResult);

            var mockHttpContext = new Mock<HttpContext>();
            var mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.Setup(req => req.Scheme).Returns("HTTPS");
            mockHttpRequest.Setup(req => req.Host).Returns(new HostString("myserver.com"));
            mockHttpRequest.Setup(req => req.Path).Returns("/customer");
            mockHttpContext.Setup(ctx => ctx.Request).Returns(mockHttpRequest.Object);
            _customerController = new CustomerController(_loggerMock.Object, _customerServiceMock.Object, _validatorMock.Object);
            _customerController.ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };
        }

        [Fact]
        public async void GetAllCustomers_GivenServiceReturnsEmptyList_ShouldReturnEmptyListOfCustomers()
        {
            // arrange
            var customers = new List<Customer>();
            _customerServiceMock.Setup(x => x.GetCustomersAsync()).ReturnsAsync(customers);
            // act
            var actual = await _customerController.GetCustomers();

            // assert
            Assert.NotNull(actual);
            var objectResult = actual as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(customers, objectResult.Value);
        }

        [Fact]
        public async void GetAllCustomers_GivenServiceReturnsCustomers_ShouldReturnCustomers()
        {
            // arrange
            var customers = new List<Customer> { _customer };
            _customerServiceMock.Setup(x => x.GetCustomersAsync()).ReturnsAsync(customers);
            // act
            var actual = await _customerController.GetCustomers();

            // assert
            Assert.NotNull(actual);
            var objectResult = actual as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(customers, objectResult.Value);
        }

        [Fact]
        public async void GetCustomer_GivenInValidCustomerIdProvided_ShouldReturnCustomer()
        {
            // arrange
            // act
            var actual = await _customerController.GetCustomer(_customer.Id);

            // assert
            Assert.NotNull(actual);
            var objectResult = actual as NotFoundObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal($"Customer with Id:{_customer.Id} not found", objectResult.Value);
        }

        [Fact]
        public async void GetCustomer_GivenValidCustomerIdProvided_ShouldReturnCustomer()
        {
            // arrange
            _customerServiceMock.Setup(x => x.GetCustomerAsync(_customer.Id)).ReturnsAsync(_customer);
            // act
            var actual = await _customerController.GetCustomer(_customer.Id);

            // assert
            Assert.NotNull(actual);
            var objectResult = actual as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(_customer, objectResult.Value);
        }

        [Fact]
        public async void PostCustomer_GivenCustomerNameNotProvided_ShouldReturnBadObject()
        {
            // arrange
            _validationResult.Errors.Add(new ValidationFailure("", "Error Message 2"));
            _validationResult.Errors.Add(new ValidationFailure("", "Error Message 1"));
            _customer.Name = string.Empty;

            // act
            var actual = await _customerController.PostCustomer(_customer);

            // assert
            Assert.NotNull(actual);
            var objectResult = actual as BadRequestObjectResult;
            Assert.NotNull(objectResult);
        }

        [Fact]
        public async void PostCustomer_GivenCustomerValidRequestProvided_ShouldReturnCreatedResult()
        {
            // arrange
            _customerServiceMock.Setup(x => x.CreateCustomerAsync(_customer)).ReturnsAsync(_customer.Id);

            // act
            var actual = await _customerController.PostCustomer(_customer);

            // assert
            Assert.NotNull(actual);
            var createdResult = actual as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Contains(_customer.Id, createdResult.Location);
            _customerServiceMock.Verify(v => v.CreateCustomerAsync(_customer), Times.Once);
        }

        [Fact]
        public async void PutCustomer_GivenCustomerValidRequestProvided_ShouldReturnCreatedResult()
        {
            // arrange
            _customerServiceMock.Setup(x => x.UpdateCustomerAsync(_customer.Id, _customer)).ReturnsAsync(true);

            // act
            var actual = await _customerController.UpdateCustomer(_customer.Id, _customer);

            // assert
            Assert.NotNull(actual);
            var objectResult = actual as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(true, objectResult.Value);
        }

        [Fact]
        public async void DeleteCustomer_GivenInValidCustomerIdProvided_ShouldReturnCustomer()
        {
            // arrange
            _customerServiceMock.Setup(x => x.DeleteCustomerAsync(_customer.Id)).ReturnsAsync(false);

            // act
            var actual = await _customerController.DeleteCustomer(_customer.Id);

            // assert
            Assert.NotNull(actual);
            var objectResult = actual as NotFoundObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal($"Customer with Id:{_customer.Id} not found", objectResult.Value);
        }

        [Fact]
        public async void DeleteCustomer_GivenValidCustomerIdProvided_ShouldReturnCustomer()
        {
            // arrange
            _customerServiceMock.Setup(x => x.DeleteCustomerAsync(_customer.Id)).ReturnsAsync(true);

            // act
            var actual = await _customerController.DeleteCustomer(_customer.Id);

            // assert
            Assert.NotNull(actual);
            var objectResult = actual as OkObjectResult;
            Assert.NotNull(objectResult);
        }
    }
}
