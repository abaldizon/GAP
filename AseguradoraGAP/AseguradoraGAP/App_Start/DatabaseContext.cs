using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AseguradoraGAP.App_Start
{
    public class DatabaseContext
    {
        MongoClient _client;
        MongoServer _server;

        public MongoDatabase _database;

        public DatabaseContext()
        {            
            var MongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"];
            var MongoUsername = ConfigurationManager.AppSettings["MongoUsername"];  
            var MongoPassword = ConfigurationManager.AppSettings["MongoPassword"];
            var MongoPort = ConfigurationManager.AppSettings["MongoPort"];
            var MongoHost = ConfigurationManager.AppSettings["MongoHost"];
            /*
            var credential = MongoCredential.CreateMongoCRCredential(MongoDatabaseName,MongoUsername,MongoPassword);

            var settings = new MongoClientSettings
            {
                Credentials = new[] { credential },
                Server = new MongoServerAddress(MongoHost, Convert.ToInt32(MongoPort))
            };
            _client = new MongoClient(settings+ "?connect=replicaSet");*/
            _client = new MongoClient("mongodb://UserAdmin:password@localhost:27017/Seguros");
            _server = _client.GetServer();
            _database = _server.GetDatabase(MongoDatabaseName);
        }
    }
}