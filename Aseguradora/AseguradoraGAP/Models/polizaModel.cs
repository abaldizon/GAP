﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AseguradoraGAP.Models
{
    public class PolizaModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("nombre")]
        public string nombre { get; set; }
        [BsonElement("descripcion")]
        public string descripcion { get; set; }
        [BsonElement("tipoCobertura")]
        public string tipoCobertura { get; set; }
        [BsonElement("cobertura")]
        public string cobertura { get; set; }
        [BsonElement("inicioVigencia")]
        public string inicioVigencia { get; set; }
        [BsonElement("periodo")]
        public string periodo { get; set; }
        [BsonElement("precio")]
        public string precio { get; set; }
        [BsonElement("riesgo")]
        public string riesgo { get; set; }
    }
}