using LINQefNWLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQefNWLibrary.Controllers {

    public class OrderController {

        private readonly AppDbContext _context = null!;
        public OrderController(AppDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAll() {
            return await _context.Orders.OrderBy(o => o.OrderId).ToListAsync();
        }

        public async Task<Order?> GetByPk(int orderId) {
            return await _context.Orders.FindAsync(orderId);

        }

        public async Task<Order> Insert(Order order) {
            if (order.OrderId != 0) {
                throw new ArgumentException("Inserting new order requires orderId to be zero.");
            }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
        public async Task Update(int orderId, Order order) {
            if (orderId != order.OrderId) {
                throw new Exception("This customer does not exist or match; please run an Insert function instead.");
            }
            _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified; // Gets EF to recognize there has been a change so it can be saved and updated
            await _context.SaveChangesAsync();
            return; //why didn't i need to return customer here again? Redundant.
        }

        public async Task Delete(int orderId) {
            Order? ord = await GetByPk(orderId);
            if (ord is null) {
                throw new Exception("Order not found.");
            }
            _context.Orders.Remove(ord);
            await _context.SaveChangesAsync();
        }
    }
}
