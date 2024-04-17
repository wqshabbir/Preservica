using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> UpdateCustomerAsync(string customerId, Customer customer)
        {
            return await _customerRepository.UpdateCustomerAsync(customerId, customer);
        }
        public async Task<string> CreateCustomerAsync(Customer customer)
        {
            string customerId = Guid.NewGuid().ToString();
            customer.Id = customerId;
            await _customerRepository.CreateCustomerAsync(customer);
            return customerId;
        }

        public async Task<bool> DeleteCustomerAsync(string customerId)
        {
            return await _customerRepository.DeleteCustomerAsync(customerId);
        }

        public async Task<Customer> GetCustomerAsync(string customerId)
        {
            return await _customerRepository.GetCustomerAsync(customerId);
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _customerRepository.GetCustomersAsync();
        }
    }
}
