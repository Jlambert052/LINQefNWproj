using LINQefNWLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQefNWLibrary.Controllers {

    public class EmployeesController {

        private readonly AppDbContext _context = null!;
        public EmployeesController(AppDbContext context) {
            _context = context;
        }

        public IEnumerable<Employee> GetAll() {
            return _context.Employees.OrderBy(e => e.LastName).ToList();
        }

        public Employee? GetByPk(int employeeId) {
            //Employee? emp = _context.Employees.SingleOrDefault(e => e.EmployeeId == employeeId);
            return _context.Employees.Find(employeeId); //find only works with primary keys but allows tighter/easier code
        }
        
        public IEnumerable<Employee> GetByLastNameStr(string subString) {
            IEnumerable<Employee> employees = from e in _context.Employees
                                              where e.LastName.Contains(subString)
                                              orderby e.LastName
                                              select e;
            return employees;
        }

        public void Update(int employeeId, Employee employee) {
            if (employeeId != employee.EmployeeId) {
                throw new ArgumentException("Invalid Id entered; does not match any instance.");

            }
            _context.Entry(employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified; //Going to ask a question about this line again
            _context.SaveChanges(); //finish update method with savechanges to write to db.
            return;
        }

        public Employee Insert(Employee employee) {
            if (employee.EmployeeId != 0) {
                throw new ArgumentException("Inserting a new employee requires employeeId to be set to zero.");
            }
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }
        public void Delete(int employeeId) {
            Employee? empl = GetByPk(employeeId);
            if (empl is null) {
                throw new Exception("Employee not found");
            }
            _context.Remove(empl);
            _context.SaveChanges();
        }
    }
}
