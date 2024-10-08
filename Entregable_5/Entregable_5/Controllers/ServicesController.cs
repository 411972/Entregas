using Microsoft.AspNetCore.Mvc;
using TurnosBack.Data.Contracts;
using TurnosBack.Data.Models;

namespace Entregable_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : Controller
    {
        private readonly IServicesService _servicesService;

        public ServicesController(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }


        [HttpGet]
        [Route("all")]
        public IActionResult GetAllServices()
        {
            return Ok(_servicesService.GetAll());
        }

        [HttpGet]
        [Route("filter")]
        public IActionResult GetService(int precio_min, int precio_max)
        {
            try
            {
                var service = _servicesService.Get(precio_min, precio_max);

                if (service == null)
                {
                    return NotFound("No hubo coincidencia");
                }
                else
                {
                    return Ok(service);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "error");
            }
        }

        [HttpPost]

        public IActionResult CrearServicio([FromBody] TServicio servicio)
        {
            if (servicio != null) {
                try
                {
                    _servicesService.Create(servicio);
                    return Ok("Se agregó el servicio");
                }
                catch (Exception)
                {
                    return StatusCode(500, "Error");
                    throw;
                }
            }
            else
            {
                return BadRequest();
            }

        }


        [HttpPut]
        [Route("edit/{id}")]

        public IActionResult UpdateService(int id, [FromBody] TServicio servicioNuevo)
        {
            try
            {
                if (_servicesService.UpdateService(id, servicioNuevo))
                {
                    return Ok("Se ha actualizado el servicio.");
                }
                else
                {
                    return BadRequest("No se pudo");
                }
            }
            catch (Exception exc)
            {
                return StatusCode(500, "error");
                throw;
            }
        }

        [HttpPut]
        [Route("delete/{id}")]

        public IActionResult DeleteService(int id)
        {
            try
            {
                if (_servicesService.Delete(id))
                {
                    return Ok("Se ha hecho la baja lógica");
                }
                else
                {
                    return BadRequest("No se pudo");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Hubo un error");
                throw;
            }
        }

    }
}