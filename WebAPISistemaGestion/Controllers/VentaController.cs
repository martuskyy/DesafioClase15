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
        public IActionResult CrearVenta(int idUsuario, [FromBody] List<Producto> productos)
        {
            if (productos.Count == 0)
            {
                return BadRequest(new { mensaje = "No se puede crear la venta por falta de productos seleccionados", status = HttpStatusCode.BadRequest });
            }

            try
            {
                VentaData venta = new VentaData();
                bool ventaAgregada = venta.AgregarNuevaVenta(idUsuario, productos);

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

        [HttpDelete("{id}")]
        public IActionResult EliminarVenta(int id)
        {
            if(id<0)
            {
                return BadRequest(new { mensaje = "El id de la venta no puede ser menor que cero", status = HttpStatusCode.BadRequest });
            }

            try
            {
                Venta venta = VentaData.ObtenerVentas(id);

                List<ProductoVendido> productosVendidos = ProductoVendidoData.ListarProductosVendidosPorIDVenta(id);

                foreach (ProductoVendido productoVendido in productosVendidos)
                {
                    Producto producto = ProductoData.ObtenerProductos(productoVendido.IdProducto);
                    producto.Stock += productoVendido.Stock;
                    ProductoData.ModificarProductos(producto.Id, producto);
                    ProductoVendidoData.EliminarProductoVendido(productoVendido.Id);
                }

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
