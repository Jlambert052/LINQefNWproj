using LINQefNWLibrary.Controllers;
using LINQefNWLibrary.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Net.Sockets;

Console.WriteLine("Hello, Northwind LINQ EF Library!");

AppDbContext _context = new();

EmployeesController empCtrl = new(_context);
CustomerController custCtrl = new(_context);


var cust = custCtrl.GetByPk("ANTON");

Print(cust);

Customer[] custs = custCtrl.GetAll().ToArray();

foreach(Customer cst in custs) {
    Print(cst);
}

/*
Customer? x5 = custCtrl.GetByPk("MICRO");

x5.Address = "123 Micro Way";

custCtrl.Update(x5.CustomerId, x5);
/*

/*
Customer? newCust = new() {
    CustomerId = "MICRO", CompanyName = "MicroCenter", ContactName = "John MicroCenter", ContactTitle = "CEO"
};

custCtrl.Insert(newCust);
*/

/*
custCtrl.Update("MICRO", ) {

}
*/

/*
var empls = empCtrl.GetByLastNameStr("ll");

foreach(Employee emp in empls) {
    Print(emp);
}
*/
/*
Employee? newEmp1 = new() {
    EmployeeId = 0, LastName = "Dover", FirstName = "Ben",
    Title = "Overlord", TitleOfCourtesy = "Mr.", BirthDate = new DateTime(2022, 9, 11),
    HireDate = new DateTime(2022, 9, 12)
};
*/

//empCtrl.Delete(10);



//Employee[] empls = empCtrl.GetAll().ToArray();

//foreach(Employee emp in empls) {
//    Print(emp);
//}


void Print(object obj) {
    if(obj is null) {
        obj = "NULL";
    }
    Console.WriteLine(obj);
    System.Diagnostics.Debug.WriteLine(obj.ToString());
}
