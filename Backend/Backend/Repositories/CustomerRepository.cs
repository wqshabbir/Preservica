using Backend.Models;
using Backend.Repositories.Base;
using Backend.Repositories.Interfaces;

namespace Backend.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration) : base(configuration) { }
        public async Task<bool> CreateCustomerAsync(Customer customer)
        {
            const string query = @"INSERT INTO CUSTOMER 
                                   (   
                                       CUSTOMER_ID, 
                                       NAME, 
                                       POSTAL_ADDRESS, 
                                       EMAIL, 
                                       PHONE_NUMBER 
                                   )
                                   VALUES
                                   ( 
                                       :id,
                                       :name,
                                       :postalAddress,
                                       :email,
                                       :phoneNumber
                                   )";

            var result = await ExecuteAsync(query, new
            {
                id = customer.Id,
                name = customer.Name,
                postalAddress = customer.PostalAddress,
                emailAddress = customer.Email,
                phoneNumber = customer.PhoneNumber
            });

            return result > 0;
        }

        public async Task<bool> DeleteCustomerAsync(string Id)
        {
            const string query = @"DELETE FROM CUSTOMER
                                   WHERE CUSTOMER_ID   =   :customerId";
            var result = await ExecuteAsync(query, new
            {
                customerId = Id
            });

            return result > 0;
        }

        public async Task<Customer> GetCustomerAsync(string Id)
        {
            const string query = @"SELECT * FROM CUSTOMER
                                   WHERE CUSTOMER_ID   =    :customerId";
            return await QueryFirstOrDefaultAsync(query, new { customerId = Id });
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            const string query = @"SELECT * FROM CUSTOMER";
            return await QueryAsync(query);
        }

        public async Task<bool> UpdateCustomerAsync(string id, Customer customer)
        {
            const string query = @"UPDATE CUSTOMER 
                                   SET NAME             =   :name  
                                       POSTAL_ADDRESS   =   :postalAddress, 
                                       EMAIL            =   :email, 
                                       PHONE_NUMBER     =   :phoneNumber
                                   WHERE CUSTOMER_ID    =   :customerId";

            var result = await ExecuteAsync(query, new
            {
                customerId = id,
                name = customer.Name,
                postalAddress = customer.PostalAddress,
                emailAddress = customer.Email,
                phoneNumber = customer.PhoneNumber
            });

            return result > 0;
        }
    }
}
