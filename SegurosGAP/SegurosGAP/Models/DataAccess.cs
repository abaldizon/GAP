using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;

namespace SegurosGAP.Models
{
    public class DataAccess
    {
        MongoClient _client;
        MongoServer _server;
        MongoDatabase _db;

        public DataAccess()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _server = _client.GetServer();
            _db = _server.GetDatabase("Seguros");
        }

        public IEnumerable<Poliza> GetPolizas()
        {
            return _db.GetCollection<Poliza>("Polizas").FindAll();
        }


        public Poliza GetPoliza(ObjectId id)
        {
            var res = Query<Poliza>.EQ(p => p.Id, id);
            return _db.GetCollection<Poliza>("Polizas").FindOne(res);
        }

        public Poliza Create(Poliza p)
        {
            _db.GetCollection<Poliza>("Polizas").Save(p);
            return p;
        }

        public void Update(ObjectId id, Poliza p)
        {
            p.Id = id;
            var res = Query<Poliza>.EQ(pd => pd.Id, id);
            var operation = Update<Poliza>.Replace(p);
            _db.GetCollection<Poliza>("Polizas").Update(res, operation);
        }
        public void Remove(ObjectId id)
        {
            var res = Query<Poliza>.EQ(e => e.Id, id);
            var operation = _db.GetCollection<Poliza>("Polizas").Remove(res);
        }
    }
}