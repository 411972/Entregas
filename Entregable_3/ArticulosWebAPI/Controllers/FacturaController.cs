using ArticulosBack.Models;
using ArticulosBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArticulosWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : Controller
    {
        private IFacturaService _service;

        public FacturaController()
        {
            _service = new FacturaService();
        }

        [HttpGet]

        public IActionResult GetFacturas(DateTime? f, int? fp) {

            try
            {
                var list = new List<Factura>();

                if ( f != null && fp != null)
                {
                     list = _service.ConsultarFacturas(f, fp);
                }
                if(f != null && fp == null)
                {
                     list = _service.ConsultarFacturas(f, null);
                }
                if (f == null && fp != null)
                {
                     list = _service.ConsultarFacturas(null, fp);
                }
                if(f == null && fp == null){
                     list = _service.ConsultarFacturas(null, null);
                }

                

                if(list.Count>1)
                {
                    return Ok(list);
                }
                else
                {
                    return BadRequest("No se encontraron coincidencias");
                }
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Error");
                throw exc;
            }
        }

        [HttpPost]

        public IActionResult PostFactura([FromBody]Factura f)
        {
            try
            {
                if (_service.CrearFactura(f))
                {
                    return Ok("Factura creada");
                }
                else
                {
                    return BadRequest("No se pudo.");
                }
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Error");
                throw exc;
            }
        }

        [HttpPut]

        public IActionResult PutFactura([FromBody] Factura f)
        {
            try
            {
                if (_service.EditarFactura(f))
                {
                    return Ok("Factura editada");
                }
                else
                {
                    return BadRequest("No se pudo.");
                }
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Error");
                throw exc;
            }
        }
    }
}
