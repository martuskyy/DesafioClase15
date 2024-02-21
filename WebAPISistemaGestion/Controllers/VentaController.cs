using SistemaGestionData;
using SistemaGestionEntities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPISistemaGestion.Controllers
{
    public class VentaController : Controller
    {
        [HttpPost("idUsuario")]
        public IActionResult CrearVenta(int idUsuario, [FromBody] List<Producto> productos)
        {
            if (productos.Count == 0)
            {
                return BadRequest(new { mensaje = "No se puede crear la venta por falta de productos seleccionados", status = HttpStatusCode.BadRequest });
            }

            try
            {
                VentaData ventaService = new VentaData();
                bool ventaAgregada = ventaService.AgregarNuevaVenta(idUsuario, productos);

                if (ventaAgregada)
                {
                    return Created(nameof(CrearVenta), new { mensaje = "Venta realizada correctamente", status = HttpStatusCode.Created, nuevaVenta = productos });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = "Error al agregar la venta", status = HttpStatusCode.InternalServerError });
                }
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message, status = HttpStatusCode.Conflict });
            }
        }

    }
}
