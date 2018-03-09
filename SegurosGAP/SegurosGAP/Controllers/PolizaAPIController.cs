using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SegurosGAP.Models;
using MongoDB.Bson;

namespace SegurosGAP.Controllers
{
        [Route("api/Poliza")]
        public class PolizaAPIController : Controller
        {
            DataAccess objds;

            public PolizaAPIController()
            {
                objds = new DataAccess();
            }

            [HttpGet]
            public IEnumerable<Poliza> Get()
            {
                return objds.GetPolizas();
            }
            [HttpGet("{id:length(24)}")]
            public IActionResult Get(string id)
            {
                var Poliza = objds.GetPoliza(new ObjectId(id));
                if (Poliza == null)
                {
                    return NotFound();
                }
                return new ObjectResult(Poliza);
            }

            [HttpPost]
            public IActionResult Post([FromBody]Poliza p)
            {
                objds.Create(p);
                return new OkObjectResult(p);
            }
            [HttpPut("{id:length(24)}")]
            public IActionResult Put(string id, [FromBody]Poliza p)
            {
                var recId = new ObjectId(id);
                var Poliza = objds.GetPoliza(recId);
                if (Poliza == null)
                {
                    return NotFound();
                }

                objds.Update(recId, p);
                return new OkResult();
            }

            [HttpDelete("{id:length(24)}")]
            public IActionResult Delete(string id)
            {
                var Poliza = objds.GetPoliza(new ObjectId(id));
                if (Poliza == null)
                {
                    return NotFound();
                }

                objds.Remove(Poliza.Id);
                return new OkResult();
            }
        }
    }