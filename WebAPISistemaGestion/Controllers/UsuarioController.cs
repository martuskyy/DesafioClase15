using Microsoft.AspNetCore.Mvc;
using SistemaGestionEntities;
using SistemaGestionData;
using System.Collections.Generic;

namespace WebAPISistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        [HttpPut("{id}")]
        public IActionResult ModificarUsuarioPorId(int id, [FromBody] Usuario usuario)
        {
            try
            {
                UsuarioData.ModificarUsuarios(id, usuario);
                return Ok("Usuario modificado exitosamente");   
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al modificar usuario: {ex.Message}");
            }
        }
    }
}
