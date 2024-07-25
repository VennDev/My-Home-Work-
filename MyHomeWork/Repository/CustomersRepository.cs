using System.Collections.Generic;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MyHomeWork.IRepository;
using MyHomeWork.Models;

namespace MyHomeWork.Repository
{
    public class CustomersRepository : ICustomersRepository
    {
        
        private MongoClient _client = null;
        private IMongoDatabase _database = null;
        private IMongoCollection<Customers> _collection = null;
        
        public CustomersRepository()
        {
            _client = Provider.GetClient();
            _database = _client.GetDatabase("test");
            _collection = _database.GetCollection<Customers>("customers");
        }
        
        public Customers GetCustomer(string id)
        {
            return _collection.Find(c => c.CustomerId == id).Limit(100).FirstOrDefault();
        }
        
        public List<Customers> GetCustomers()
        {
            return _collection.Find(FilterDefinition<Customers>.Empty).ToList();
        }
        
        public void AddCustomer(Customers customer)
        {
            _collection.InsertOne(customer);
        }
        
        public void UpdateCustomer(Customers customer)
        {
            var filter = Builders<Customers>.Filter.Eq(c => c.CustomerId, customer.CustomerId);
            var update = Builders<Customers>.Update
                .Set(c => c.FirstName, customer.FirstName)
                .Set(c => c.LastName, customer.LastName)
                .Set(c => c.Company, customer.Company)
                .Set(c => c.City, customer.City)
                .Set(c => c.Country, customer.Country)
                .Set(c => c.Phone1, customer.Phone1)
                .Set(c => c.Phone2, customer.Phone2)
                .Set(c => c.Email, customer.Email)
                .Set(c => c.SubscriptionDate, customer.SubscriptionDate)
                .Set(c => c.Website, customer.Website);

            _collection.UpdateOne(filter, update);
        }
        
        public void DeleteCustomer(string id)
        {
            _collection.DeleteOne(c => c.CustomerId == id);
        }
        
    }
}