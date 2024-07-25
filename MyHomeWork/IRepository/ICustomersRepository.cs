using MyHomeWork.Models;
using System.Collections.Generic;

namespace MyHomeWork.IRepository
{
    public interface ICustomersRepository
    {
        
        Customers GetCustomer(string id);
        
        List<Customers> GetCustomers();
        
        void AddCustomer(Customers customer);
        
        void UpdateCustomer(Customers customer);
        
        void DeleteCustomer(string id);
        
    }
}