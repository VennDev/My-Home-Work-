using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyHomeWork.Models
{
    public class Customers
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        
        [BsonElement("Index")]
        public Int32 Index { get; set; }
        
        [BsonElement("Customer Id")]
        [Required(ErrorMessage = "This field is required.")]
        public string CustomerId { get; set; }
        
        [BsonElement("First Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string FirstName { get; set; }
        
        [BsonElement("Last Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string LastName { get; set; }
        
        [BsonElement("Company")]
        [Required(ErrorMessage = "This field is required.")]
        public string Company { get; set; }
        
        [BsonElement("City")]
        [Required(ErrorMessage = "This field is required.")]
        public string City { get; set; }
        
        [BsonElement("Country")]
        [Required(ErrorMessage = "This field is required.")]
        public string Country { get; set; }
        
        [BsonElement("Phone 1")]
        [Required(ErrorMessage = "This field is required.")]
        public string Phone1 { get; set; }
        
        [BsonElement("Phone 2")]
        [Required(ErrorMessage = "This field is required.")]
        public string Phone2 { get; set; }
        
        [BsonElement("Email")]
        [Required(ErrorMessage = "This field is required.")]
        public string Email { get; set; }
        
        [BsonElement("Subscription Date")]
        [Required(ErrorMessage = "This field is required.")]
        public DateTime SubscriptionDate { get; set; }
        
        [BsonElement("Website")]
        [Required(ErrorMessage = "This field is required.")]
        public string Website { get; set; }
    }
}