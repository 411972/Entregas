
using Microsoft.AspNetCore.Mvc;
using TurnosBack.Data.Contracts;
using TurnosBack.Data.Models;

namespace Entregable_5.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class TurnoController : Controller
    {
        private readonly ITurnoService _repository;

        public TurnoController(ITurnoService repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult CrearTurno([FromBody]TTurno turno)
        {
            if(turno == null)
            {
                return BadRequest("El turno no debe ser null");
            }

            try
            {

                if (_repository.CrearTurno(turno))
                {
                    return Ok("Se creó el turno");
                }
                else
                {
                    return BadRequest("No se pudo");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error");
                throw;
            }

        }

        [HttpGet]

        public IActionResult ConsultarTurnos([FromQuery] string cliente)
        {
            if (string.IsNullOrEmpty(cliente))
            {
                return BadRequest("Cliente no puede estar vacío.");
            }

            var lista = _repository.ConsultarTurnos(cliente);

            if(lista.Count > 0)
            {
                return Ok(lista);
            }
            else
            {
                return NotFound("No se encontraron coincidencias.");
            }
        }

        [HttpPut]

        public IActionResult EditarTurno([FromBody] TTurno turn)
        {
           if(turn == null)
            {
                return BadRequest("El turno debe estar completo.");
            }

            try
            {
                if (_repository.EditarTurno(turn))
                {
                    return Ok("Se editó el turno");
                }
                else
                {
                    return BadRequest("No se pudo");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error");
                throw;
            }
        }

        [HttpDelete]

        public IActionResult CancelarTurno([FromQuery] int id) {
            
            if(id == null || id == 0)
            {
                return BadRequest("El id no puede ser vacio o 0");
            }

            try
            {
                if (_repository.EliminarTurno(id))
                {
                    return Ok("Se canceló el turno");
                }
                else
                {
                    return BadRequest("No se encontró el turno.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error");
                throw;
            }
        }
    }
}
