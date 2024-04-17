using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services;
using Moq;

namespace Backend.Tests.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;

        private CustomerService _customerService;
        public CustomerServiceTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _customerService = new CustomerService(_customerRepositoryMock.Object);
        }

        [Fact]
        public async void GetCustomerAsync_GivenCustomerIdProvided_CallsCustomerRepository()
        {
            // arrange
            string customerId = "customerId";
            _customerRepositoryMock.Setup(x => x.GetCustomerAsync(customerId)).ReturnsAsync(It.IsAny<Customer>());

            // act
            var actual = await _customerService.GetCustomerAsync(customerId);

            // assert
            _customerRepositoryMock.Verify(v => v.GetCustomerAsync(customerId), Times.Once);
        }

        [Fact]
        public async void CreateCustomerAsync_GivenCustomerProvided_CallsCustomerRepository()
        {
            // arrange
            Customer customer = new Customer();
            _customerRepositoryMock.Setup(x => x.CreateCustomerAsync(customer)).ReturnsAsync(It.IsAny<bool>());

            // act
            var actual = await _customerService.CreateCustomerAsync(customer);

            // assert
            _customerRepositoryMock.Verify(v => v.CreateCustomerAsync(customer), Times.Once);
        }

        [Fact]
        public async void UpdateCustomerAsync_GivenCustomerIdAndCustomerProvided_CallsCustomerRepository()
        {
            // arrange
            string customerId = "customerId";
            Customer customer = new Customer();
            _customerRepositoryMock.Setup(x => x.UpdateCustomerAsync(customerId, customer)).ReturnsAsync(It.IsAny<bool>());

            // act
            var actual = await _customerService.UpdateCustomerAsync(customerId, customer);

            // assert
            _customerRepositoryMock.Verify(v => v.UpdateCustomerAsync(customerId, customer), Times.Once);
        }

        [Fact]
        public async void DeleteCustomerAsync_GivenCustomerIdProvided_CallsCustomerRepository()
        {
            // arrange
            string customerId = "customerId";
            _customerRepositoryMock.Setup(x => x.DeleteCustomerAsync(customerId)).ReturnsAsync(It.IsAny<bool>());

            // act
            var actual = await _customerService.DeleteCustomerAsync(customerId);

            // assert
            _customerRepositoryMock.Verify(v => v.DeleteCustomerAsync(customerId), Times.Once);
        }

        [Fact]
        public async void GetCustomersAsync_CallsCustomerRepository()
        {
            // arrange
            _customerRepositoryMock.Setup(x => x.GetCustomersAsync()).ReturnsAsync(It.IsAny<IEnumerable<Customer>>());

            // act
            var actual = await _customerService.GetCustomersAsync();

            // assert
            _customerRepositoryMock.Verify(v => v.GetCustomersAsync(), Times.Once);
        }
    }
}
