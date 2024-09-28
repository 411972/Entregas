using Microsoft.AspNetCore.Mvc;
using TurnosBack.Services;

namespace Entregable_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : Controller
    {
        private readonly IServicesService _servicesService;

        public ServicesController(IServicesService servicesService)
        {
            _servicesService= servicesService;
        }

        [HttpGet]
        public IActionResult GetAllServices()
        {
            return Ok(_servicesService.GetAll());
        }
    }
}
