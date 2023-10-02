using System.Collections.Generic;
using System.Linq;
using HotelBooking.Core.Entities;
using HotelBooking.Core.Interfaces;
namespace HotelBooking.UnitTests.Fakes {
    public class FakeCustomerRepository  : IRepository<Customer>
    {

        public IEnumerable<Customer> GetAll() {
            List<Customer> customers = new() {
                new Customer { Id=1, Name= "Bo Benson" , Email = "BB@mail.com"},
                new Customer { Id=2, Name= "Joe Johnson" , Email = "JoJo@mail.com"},
            };
            return customers;
        }
        
        public Customer Get(int id) {
            List<Customer> customers = new() {
                new Customer { Id=1, Name= "Bo Benson" , Email = "BB@mail.com"},
                new Customer { Id=2, Name= "Joe Johnson" , Email = "JoJo@mail.com"},
            };
            return customers.FirstOrDefault(c => c.Id == id);
        }
        
        public void Add(Customer entity) {
        }
        
        public void Edit(Customer entity) {
        }
        
        public void Remove(int id) {
        }
    }
}
