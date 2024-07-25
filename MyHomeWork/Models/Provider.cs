using System;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MyHomeWork.Models
{
    public class Provider
    {

        private static readonly string ConnectionString = ConfigurationManager.AppSettings["MongoDB_Uri"];

        public static bool CanConnect() { return ConnectionString != ""; }
        
        public static MongoClient GetClient() 
        {
            if (ConnectionString == "") Console.WriteLine("Please set 'MongoDB_Uri' environment variable. To learn how to set it, see https://www.mongodb.com/docs/drivers/csharp/current/quick-start/#set-your-connection-string");
            
            return new MongoClient(ConnectionString);
        }

        public static IMongoCollection<BsonDocument> GetCollection(string database, string collection)
        {
            return GetClient().GetDatabase(database).GetCollection<BsonDocument>(collection);
        }

        public static BsonDocument GetDocument(string database, string collection, string field, string value)
        {
            return GetCollection(database, collection).Find(Builders<BsonDocument>.Filter.Eq(field, value)).First();
        }

    }
}