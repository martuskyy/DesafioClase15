using Microsoft.AspNetCore.Mvc;
using SistemaGestionEntities;
using SistemaGestionData;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WebAPISistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private static string connectionString = "Server=.; Database=master; Trusted_Connection=True;";

        [HttpGet("iniciarSesion/{nombreUsuario}/{clave}")]
        private IActionResult IniciarSesion(string nombreUsuario, string clave)
        {
            string query = "SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario AND Contrasena = @Contrasena";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                    command.Parameters.AddWithValue("@Clave", clave);
                    try
                    {
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            return Ok("Inicio de sesión exitoso");
                        }
                        else
                        {
                            return BadRequest("Nombre de usuario o contraseña incorrectos");
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Error: {ex.Message}");
                    }                    
                }
            }
        }

        [HttpGet("traerUsuario/{nombreUsuario}")]
        private IActionResult TraerUsuario(string nombreUsuario)
        {
            string query = "SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var usuario = new
                                {
                                    Id = reader["Id"],
                                    Nombre = reader["Nombre"],
                                    Apellido = reader["Apellido"],
                                    Mail = reader["Mail"],
                                    Clave = reader["Clave"]
                                };

                                return Ok(usuario);
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Error: {ex.Message}");
                    }
                }
            }
        }

        [HttpPost("crearUsuario")]
        private IActionResult CrearUsuario(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrEmpty(usuario.Apellido) || string.IsNullOrEmpty(usuario.NombreUsuario) ||
                string.IsNullOrEmpty(usuario.Clave) || string.IsNullOrEmpty(usuario.Email))
            {
                return BadRequest("Todos los campos son obligatorios");
            }

            IActionResult usuarioExistente = TraerUsuario(usuario.NombreUsuario);
            if (usuarioExistente is OkResult)
            {
                return BadRequest("El nombre de usuario ya existe");
            }

            try
            {
                UsuarioData.CrearUsuario(usuario);
                return Ok("Usuario creado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear usuario: {ex.Message}");
            }
        }

        [HttpPut("modificarUsuario/{id}")]
        private IActionResult ModificarUsuarioPorId(int id, [FromBody] Usuario usuario)
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

        [HttpDelete("eliminarUsuario/{id}")]
        private IActionResult EliminarUsuario(int id)
        {
            try
            {
                UsuarioData.EliminarUsuario(id);
                return Ok("Usuario eliminado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar usuario: {ex.Message}");
            }
        }
    }
}
