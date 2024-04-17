using Backend.Models;

namespace Backend.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerAsync(string customerId);
        Task<bool> CreateCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(string customerId, Customer customer);
        Task<bool> DeleteCustomerAsync(string customerId);
    }
}
