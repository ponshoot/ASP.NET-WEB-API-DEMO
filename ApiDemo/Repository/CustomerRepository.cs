using ApiDemo.Dto;
using ApiDemo.Models;
using System.Runtime.InteropServices;

namespace ApiDemo.Repository
{
    public class CustomerRepository
    {
        private readonly NorthwindContext _context;
        public CustomerRepository(NorthwindContext context)
        {
            _context = context;
        }
        public IEnumerable<Customer> Get(string? keyword = null)
        {
            return (keyword == null) ?
               _context.Customers.ToList()
               : _context.Customers.Where(c => c.CustomerId.Contains(keyword)).ToList();
        }
        public Customer? GetById(string id)
        {
            return _context.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
        }
        public void Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
        public void Update(Customer customerUpdate) 
        {
            var customer = this.GetById(customerUpdate.CustomerId);
            if (customer == null)
                throw new Exception("customer為空");
            _context.Customers.Remove(customer);
            _context.Customers.Add(customerUpdate);
            _context.SaveChanges();
        }
        public void Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
        public bool IsIdExist(string? id)
        {
            return this.Get().Where(c => c.CustomerId == id).Any();
        }
    }
}
