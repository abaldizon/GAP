using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace AseguradoraGAP.Models
{
    public class ClienteModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("cliente")]
        public string Cliente { get; set; }
        [BsonElement("Seguros")]
        public IEnumerable<string> Seguros { get; set; }
    }
}