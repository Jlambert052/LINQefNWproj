using LINQefNWLibrary.Models;
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

        public IEnumerable<Order> GetAll() {
            return _context.Orders.OrderBy(o => o.OrderId);
        }

        public Order? GetByPk(int orderId) {
            return _context.Orders.Find(orderId);

        }

        public Order Insert(Order order) {
            if (order.OrderId != 0) {
                throw new ArgumentException("Inserting new order requires orderId to be zero.");
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }
        public void Update(int orderId, Order order) {
            if (orderId != order.OrderId) {
                throw new Exception("This customer does not exist or match; please run an Insert function instead.");
            }
            _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified; // Gets EF to recognize there has been a change so it can be saved and updated
            _context.SaveChanges();
            return; //why didn't i need to return customer here again? Redundant.
        }

        public void Delete(int orderId) {
            Order? ord = GetByPk(orderId);
            if (ord is null) {
                throw new Exception("Order not found.");
            }
            _context.Orders.Remove(ord);
            _context.SaveChanges();
        }
    }
}
