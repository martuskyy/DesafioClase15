using Microsoft.AspNetCore.Mvc;
using SistemaGestionData;
using SistemaGestionEntities;
using System.Net;

namespace WebAPISistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        [HttpPost("crear")]
        public IActionResult CrearProducto([FromBody] Producto producto)
        {
            try
            {
                if (producto == null || string.IsNullOrEmpty(producto.Descripcion) || producto.Costo <= 0 || producto.PrecioVenta <= 0 ||
                    producto.Stock < 0 || producto.IdUsuario <= 0)
                {
                    return BadRequest("Datos de producto no válidos, imposible crear el mismo");
                }

                ProductoData.CrearProducto(producto);
                return Ok("Producto creado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear producto: {ex.Message}");
            }

        }

        [HttpPut("modificar/{id}")]
        public IActionResult ModificarProducto(int id, [FromBody] Producto producto)
        {
            try
            {
                if (producto == null || producto.Id != id)
                {
                    return BadRequest("Datos de producto no válidos");
                }

                ProductoData.ModificarProductos(id, producto);

                return Ok("Producto modificado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al modificar producto: {ex.Message}");
            }
        }

        [HttpDelete("eliminar/{id}")]
        public IActionResult EliminarProducto(int id)
        {
            if (id < 0)
            {
                return BadRequest(new { message = "El id no puede ser negativo", status = HttpStatusCode.BadRequest });
            }
            try
            {
                ProductoVendidoData.EliminarProductoVendido(id);
                ProductoData.EliminarProducto(id);
                return Ok("Producto eliminado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar producto: {ex.Message}");
            }
        }

        [HttpGet("traer")]
        public IActionResult TraerProductos()
        {
            try
            {
                ProductoData.ListarProductos();
                return Ok();
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message, status = HttpStatusCode.Conflict });
            }
        }
    }
}
