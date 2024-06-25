using System.Collections.Generic;
using System.Globalization;
using CustomerAccountManagement.DomainServices.Interfaces;
using CustomerAccountManagement.Domain.Entities;
using System.Threading.Tasks;
using CsvHelper;

namespace CustomerAccountManagement.DomainServices.Services
{
    public class CustomerService(IRepository<Customer> repository, ICustomerRepository customerRepository) : ICustomerService
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await repository.GetAll();
        }

        public async Task<Customer?> GetCustomerById(Guid customerId)
        {
            return await repository.GetById(customerId);
        }

        public async Task CreateCustomer(Customer customer)
        {
            await repository.Create(customer);
        }

        public Task UpdateCustomer(Guid id, Customer customer)
        {
            return repository.Update(id, customer);
        }

        public async Task DeleteCustomer(Guid id)
        {
            var customer = await repository.GetById(id);
            if (customer != null) await repository.Delete(customer);
        }

        public async Task ImportExternalCustomers()
        {
            var url = "https://marcavans.blob.core.windows.net/solarch/fake_customer_data_export.csv?sv=2023-01-03&st=2024-06-14T10%3A31%3A07Z&se=2032-06-15T10%3A31%3A00Z&sr=b&sp=r&sig=q4Ie3kKpguMakW6sbcKl0KAWutzpMi747O4yIr8lQLI%3D";

            var response = await HttpClient.GetStringAsync(url);
            var customers = new List<Customer>();

            using var reader = new StringReader(response);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CustomerCSVDataMap>();
            var records = csv.GetRecords<CustomerCSVData>();

            foreach (var record in records)
            {
                var customer = new Customer
                {
                    Name = $"{record.FirstName} {record.LastName}",
                    Email = $"{record.FirstName.ToLower()}.{record.LastName.ToLower()}@example.com",
                    Street = GetStreet(record.Address),
                    City = GetCity(record.Address),
                    ZipCode = GetZipCode(record.Address)
                };

                customers.Add(customer);
                Console.WriteLine($"Added customer: {customer.Name}");
            }

            await customerRepository.SaveExternalCustomers(customers);
            Console.WriteLine(customers.Count + " customers added");
        }
        
        private string GetStreet(string address)
        {
            var parts = address.Split(',');
            return parts[0].Trim();
        }

        private string GetCity(string address)
        {
            var parts = address.Split(',');
            return parts[1].Trim().Split(' ')[1].Trim();
        }

        private string GetZipCode(string address)
        {
            var parts = address.Split(',');
            return parts[1].Trim().Split(' ')[0].Trim();
        }
    }
    
    
}