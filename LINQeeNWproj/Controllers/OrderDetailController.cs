using LINQefNWLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQefNWLibrary.Controllers {

    public class OrderDetailController {

        private readonly AppDbContext _context = null!;
        public OrderDetailController(AppDbContext context) {
            _context = context;
        }

        public IEnumerable<OrderDetail> GetAll() {
            return _context.OrderDetails.OrderBy(od => od.OrderId);
        }

        public OrderDetail? GetByPk(int productId, int orderId) {
            return _context.OrderDetails.Find(productId, orderId);

        }

        public OrderDetail Insert(OrderDetail orderDetail) {
            OrderDetail? od = GetByPk(orderDetail.OrderId, orderDetail.ProductId);
            if(od != null) {
                throw new Exception("Primary key already exists");
            }
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
            return orderDetail;
        }
        public void Update(int productId, int orderId, OrderDetail orderDetail) {
            if (productId != orderDetail.ProductId || orderId != orderDetail.OrderId) {
                throw new ArgumentException("This productId and orderId do not match.");
            }
            _context.Entry(orderDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified; // Gets EF to recognize there has been a change so it can be saved and updated
            _context.SaveChanges();
            return; //why didn't i need to return customer here again? Redundant.
        }

        public void Delete(int productId, int orderId) {
            OrderDetail? orde = GetByPk(productId, orderId);
            if(orde is null) {
                throw new Exception("OrderDetail not found.");
            }
            _context.OrderDetails.Remove(orde);
            _context.SaveChanges();
        }
    }
}
