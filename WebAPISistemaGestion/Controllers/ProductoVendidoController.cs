using Microsoft.AspNetCore.Mvc;
using SistemaGestionEntities;
using SistemaGestionData;
using System.Net;

namespace WebAPISistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoVendidoController : Controller
    {
        [HttpGet("obtenerVendidos/{idUsuario}")]
        private ActionResult<List<ProductoVendido>> TraerProductosVendidos(int idUsuario)
        {
            if(idUsuario<0)
            {
                return BadRequest(new { message = "El id no puede ser negativo", status = HttpStatusCode.BadRequest });
            }

            try
            {
                ProductoVendidoData.ObtenerProductosVendidos(idUsuario);
                return Ok();
            }
            catch(Exception ex)
            {
                return Conflict(new { message = ex.Message, status = HttpStatusCode.Conflict});
            }
        }
    }
}
