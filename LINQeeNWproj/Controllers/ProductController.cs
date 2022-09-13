using LINQefNWLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQefNWLibrary.Controllers {

    public class ProductController {

        private readonly AppDbContext _context = null!;
        public ProductController(AppDbContext context) {
            _context = context;
        }

        public IEnumerable<Product> GetAll() {
            return _context.Products.OrderBy(p => p.ProductId);
        }

        public Product? GetByPk(int productId) {
            return _context.Products.Find(productId);

        }

        public Product Insert(Product product) {
            _context.Products.Add(product);
            if(product.ProductId != 0) {
                throw new ArgumentException("Inserting new product detail requires the productId to be 0");
            }
            _context.SaveChanges();
            return product;
        }
        public void Update(int productId, Product product) {
            if (productId != product.ProductId) {
                throw new ArgumentException("This customer does not exist or match; please run an Insert function instead.");
            }
            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified; // Gets EF to recognize there has been a change so it can be saved and updated
            _context.SaveChanges();
            return; //why didn't i need to return customer here again? Redundant.
        }

        public void Delete(int productId) {
            Product? prod = GetByPk(productId);
            if (prod is null) {
                throw new Exception("Product not found");
            }
            _context.Products.Remove(prod);
            _context.SaveChanges();
        }
    }
}
