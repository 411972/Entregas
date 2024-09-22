using ArticulosBack.Data;
using ArticulosBack.Models;
using ArticulosBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArticulosWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : Controller
    {
        private ArticuloService service;

        public ArticuloController()
        {
            service = new ArticuloService();
        }

        [HttpGet("articulos")]
        
        public IActionResult GetArticulos([FromQuery] string? nombre) {

            try
            {
                var list = service.ObtenerArticulos(nombre);

                if (list.Count > 0) {
                    return Ok(list);
                }
                else
                {
                    return NotFound("No hubo coincidencias");
                }
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Hubo un error");
                throw exc;
            }
            
        }

        [HttpPost("articulo")]

        public IActionResult PostArticulo([FromBody] Articulo a)
        {
            try
            {
                if (service.AgregarArticulo(a))
                {
                    return Ok("Se agregó el articulo");
                }
                else
                {
                    return BadRequest("No se pudo agregar");
                }
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Hubo un error");
                throw exc;
            }

        }

        [HttpPut("articulo")]

        public IActionResult PutArticulo([FromBody] Articulo a)
        {
            try
            {
                if (service.EditarArticulo(a)){
                    return Ok("Se editó el articulo");
                }
                else
                {
                    return BadRequest("No se pudo editar");
                }
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Hubo un error");
                throw exc;
            }
        }

        [HttpDelete("articulo")]

        public IActionResult DeleteArticulo([FromQuery] int id)
        {
            try
            {
                if (service.EliminarArticulo(id))
                {
                    return Ok("Se eliminó el articulo");
                }
                else
                {
                    return BadRequest("No se pudo eliminar");
                }
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Hubo un error");
                throw exc;
            }
        }


    }
}
