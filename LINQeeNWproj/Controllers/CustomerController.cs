using LINQefNWLibrary.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Customer>> GetAll() {
            return await _context.Customers.OrderBy(c => c.CustomerId).ToListAsync();
        }

        public async Task<Customer?> GetByPk(string customerId) {
            //Customer? empl = await _context.Customers.SingleOrDefaultAsync(e => e.CustomerId == customerId);
            return await _context.Customers.FindAsync(customerId);
             
        }

        public async Task<Customer> Insert(Customer customer) {
            if (customer.CustomerId.Length != 5) {
                throw new Exception("This customerId does not match");
            }
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task Update(string customerId, Customer customer) {
            if (customerId != customer.CustomerId) {
                throw new Exception("This customer does not exist or match; please run an Insert function instead.");
            }
            _context.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified; // Gets EF to recognize there has been a change so it can be saved and updated
            await _context.SaveChangesAsync();
            return; //why didn't i need to return customer here again? Redundant.
        }

        public async Task Delete(string customerId) {
            Customer? cust = await GetByPk(customerId);
            if (cust is null) {
                throw new Exception("This customer does not exist; unable to delete something that is not there.");
            }
            _context.Customers.Remove(cust);
            await _context.SaveChangesAsync();
        }
    } 
}
