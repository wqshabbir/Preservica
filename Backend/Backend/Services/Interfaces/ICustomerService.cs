using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerAsync(string customerId);
        Task<string> CreateCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(string customerId, Customer customer);
        Task<bool> DeleteCustomerAsync(string customerId);
    }
}
