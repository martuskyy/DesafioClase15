using Microsoft.AspNetCore.Mvc;

namespace WebAPISistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NombreAPIController : Controller
    {
        [HttpGet]
        public string ObtenerNombreAPI()
        {
            return "martusky's app";
        }
    }
}
