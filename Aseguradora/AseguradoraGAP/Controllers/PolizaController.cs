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
    public class PolizaController : Controller
    {
        DatabaseContext _dbContext;
        public PolizaController ()
        {
            _dbContext = new DatabaseContext();
        }
        // GET: Poliza
        public ActionResult Index()
        {
            var polizas = _dbContext._database.GetCollection<PolizaModel>("Polizas").FindAll().ToList();
            return View(polizas);
        }

        // GET: Poliza/Details/5
        public ActionResult Details(string id)
        {
            var idPoliza = Query<PolizaModel>.EQ(p => p.Id, new ObjectId(id));
            var detallePoliza = _dbContext._database.GetCollection<PolizaModel>("Polizas").FindOne(idPoliza);
            return View(detallePoliza);
        }

        // GET: Poliza/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Poliza/Create
        [HttpPost]
        public ActionResult Create(PolizaModel poliza)
        {
            var document = _dbContext._database.GetCollection<BsonDocument>("Polizas");
            var query = Query.And(
                Query.EQ("nombre", poliza.nombre),
                Query.EQ("descripcion", poliza.descripcion),
                Query.EQ("tipoCobertura", poliza.tipoCobertura),
                Query.EQ("cobertura", poliza.cobertura),
                Query.EQ("inicioVigencia", poliza.inicioVigencia),
                Query.EQ("periodo", poliza.periodo),
                Query.EQ("precio", poliza.precio),
                Query.EQ("riesgo", poliza.riesgo));
            var count = document.FindAs<PolizaModel>(query).Count();

            if (count == 0)
            {
                var result = document.Insert(poliza);
            }
            else
            {
                TempData["Message"] = "Esta poliza ya existe";
                return View("Create", poliza);
            }
            return RedirectToAction("Index");
        }

        // GET: Poliza/Edit/5
        public ActionResult Edit(string id)
        {
            var document = _dbContext._database.GetCollection<PolizaModel>("Polizas");
            var polizasCount = document.FindAs<PolizaModel>(Query.EQ("_id", new ObjectId(id))).Count();

            if (polizasCount > 0)
            {
                var idPoliza = Query<PolizaModel>.EQ(p => p.Id, new ObjectId(id));
                var detallePoliza = _dbContext._database.GetCollection<PolizaModel>("Polizas").FindOne(idPoliza);
                return View(detallePoliza);
            }
            return RedirectToAction("Index");
        }

        // POST: Poliza/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, PolizaModel poliza)
        {
            try
            {
                poliza.Id = new ObjectId(id);
                var CarObjectId = Query<PolizaModel>.EQ(p => p.Id, new ObjectId(id));
                var collection = _dbContext._database.GetCollection<PolizaModel>("Polizas");
                var result = collection.Update(CarObjectId, Update.Replace(poliza), UpdateFlags.None);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Poliza/Delete/5
        public ActionResult Delete(string id)
        {
            var idPoliza = Query<PolizaModel>.EQ(p => p.Id, new ObjectId(id));
            var detallePoliza = _dbContext._database.GetCollection<PolizaModel>("Polizas").FindOne(idPoliza);
            return View(detallePoliza);
        }

        // POST: Poliza/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, PolizaModel poliza)
        {
            try
            {
                var idPoliza = Query<PolizaModel>.EQ(p => p.Id, new ObjectId(id));
                var collection = _dbContext._database.GetCollection<PolizaModel>("Polizas");
                var result = collection.Remove(idPoliza, RemoveFlags.Single);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
