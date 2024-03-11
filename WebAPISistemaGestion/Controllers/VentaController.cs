using SistemaGestionData;
using SistemaGestionEntities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPISistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : Controller
    {
        [HttpGet]
        public IActionResult TraerVentas()
        {
            try
            {
                List<Venta> ventas = VentaData.ListarVentas();
                return Ok(ventas);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message, status = HttpStatusCode.Conflict });
            }
        }

        [HttpPost("{idUsuario}")]
        public IActionResult CrearVenta(int idUsuario, [FromBody] Venta venta)
        {
            try
            {
                if (venta == null || string.IsNullOrEmpty(venta.Comentarios) || venta.Id < 0 || venta.IdUsuario <= 0)
                {
                    return BadRequest("Datos de venta incompletos, imposible crear la misma");
                }

                VentaData.CrearVenta(venta);
                return Ok("Venta creada exitosamente");
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message, status = HttpStatusCode.Conflict });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarVenta(int id)
        {
            if(id<0)
            {
                return BadRequest(new { mensaje = "El id de la venta no puede ser menor que cero", status = HttpStatusCode.BadRequest });
            }

            try
            {
                VentaData.EliminarVenta(id);

                return Ok("Venta eliminada exitosamente");
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message, status = HttpStatusCode.Conflict });
            }
        }
    }
}
