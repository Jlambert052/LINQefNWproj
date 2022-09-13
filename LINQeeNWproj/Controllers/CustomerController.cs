using LINQefNWLibrary.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQefNWLibrary.Controllers {

    public class CustomerController {

        private readonly AppDbContext _context = null!;
        public CustomerController(AppDbContext context) {
            _context = context;
        }

        public IEnumerable<Customer> GetAll() {
            return _context.Customers.OrderBy(c => c.CustomerId);
        }

        public Customer? GetByPk(string customerId) {
            return _context.Customers.Find(customerId);

        }

        public Customer Insert(Customer customer) {
            if (customer.CustomerId.Length != 5) {
                throw new Exception("This customerId does not match");
            }
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public void Update(string customerId, Customer customer) {
            if (customerId != customer.CustomerId) {
                throw new Exception("This customer does not exist or match; please run an Insert function instead.");
            }
            _context.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified; // Gets EF to recognize there has been a change so it can be saved and updated
            _context.SaveChanges();
            return; //why didn't i need to return customer here again? Redundant.
        }

        public void Delete(string customerId) {
            Customer? cust = GetByPk(customerId);
            if (cust is null) {
                throw new Exception("This customer does not exist; unable to delete something that is not there.");
            }
            _context.Customers.Remove(cust);
            _context.SaveChanges();
            //void methods do not need a return statement.
        }
    } 
}
