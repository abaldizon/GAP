using AseguradoraGAP.App_Start;
using AseguradoraGAP.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AseguradoraGAP.Controllers
{
    public class ClienteController : Controller
    {
        DatabaseContext _dbContext;
        public ClienteController()
        {
            _dbContext = new DatabaseContext();
        }

        // GET: Clientes
        public ActionResult Index()
        {
            var clientes = _dbContext._database.GetCollection<ClienteModel>("Clientes").FindAll().ToList();
            return View(clientes);
        }

        // GET: Clientes/Details/5
        public ActionResult Details(string id)
        {
            var idCliente = Query<ClienteModel>.EQ(p => p.Id, new ObjectId(id));
            var detalleCliente = _dbContext._database.GetCollection<ClienteModel>("Clientes").FindOne(idCliente);            
            var listaSeguros = detalleCliente.Seguros;
            return View(detalleCliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(string id)
        {
            var document = _dbContext._database.GetCollection<ClienteModel>("Clientes");
            var clientesCount = document.FindAs<ClienteModel>(Query.EQ("_id", new ObjectId(id))).Count();

            if (clientesCount > 0)
            {
                var idCliente = Query<ClienteModel>.EQ(p => p.Id, new ObjectId(id));
                var detalleCliente = _dbContext._database.GetCollection<ClienteModel>("Clientes").FindOne(idCliente);                
                return View(detalleCliente);
            }            
            return RedirectToAction("Index");
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, ClienteModel cliente)
        {
            try
            {
                cliente.Id = new ObjectId(id);
                var CarObjectId = Query<ClienteModel>.EQ(p => p.Id, new ObjectId(id));
                var collection = _dbContext._database.GetCollection<ClienteModel>("Clientes");
                var result = collection.Update(CarObjectId, Update.Replace(cliente), UpdateFlags.None);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
