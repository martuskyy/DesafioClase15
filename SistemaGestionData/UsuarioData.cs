using System.Data.SqlClient;
using SistemaGestionEntities;

namespace SistemaGestionData
{
    public class UsuarioData
    {
        private static string connectionString;

        static UsuarioData()
        {
            UsuarioData.connectionString = "Server=.; Database=master; Trusted_Connection=True;";
        }

        public static List<Usuario> ListarUsuarios()
        {
            try
            {
                List<Usuario> listadoDeUsuarios = new List<Usuario>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Usuario;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["Id"]);
                        usuario.Nombre = reader["Nombre"].ToString();
                        usuario.Apellido = reader["Apellido"].ToString();
                        usuario.NombreUsuario = reader["NombreUsuario"].ToString();
                        usuario.Clave = reader["Contraseña"].ToString();
                        usuario.Email = reader["Mail"].ToString();

                        listadoDeUsuarios.Add(usuario);

                    }
                }
                return listadoDeUsuarios;
            }

            catch (Exception ex)
            {
                throw new Exception("Error al listar los usuarios", ex);
            }
        }

        public static Usuario ObtenerUsuarios(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM USUARIO WHERE id=@id;";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("ID", id);

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader["Id"]);
                    usuario.Nombre = reader["Nombre"].ToString();
                    usuario.Apellido = reader["Apellido"].ToString();
                    usuario.NombreUsuario = reader["NombreUsuario"].ToString();
                    usuario.Clave = reader["Contraseña"].ToString();
                    usuario.Email = reader["Mail"].ToString();

                    return usuario;
                }
            }
            throw new Exception("Usuario inexistente");
        }

        public static void CrearUsuario(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Usuario (Nombre,Apellido,NombreUsuario,Contraseña,Mail)" +
                               "values (@nombre,@apellido,@nombreUsuario,@clave,@email); select @@IDENTITY as ID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("nombre", usuario.Nombre);
                command.Parameters.AddWithValue("apellido", usuario.Apellido);
                command.Parameters.AddWithValue("nombreUsuario", usuario.NombreUsuario);
                command.Parameters.AddWithValue("clave", usuario.Clave);
                command.Parameters.AddWithValue("email", usuario.Email);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static void ModificarUsuarios(int id, Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Usuario SET Nombre = @nombre,Apellido = @apellido,NombreUsuario = @nombreUsuario,Contraseña= @clave,Mail=@email " +
                               " WHERE id = @id ";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("nombre", usuario.Nombre);
                command.Parameters.AddWithValue("apellido", usuario.Apellido);
                command.Parameters.AddWithValue("nombreUsuario", usuario.NombreUsuario);
                command.Parameters.AddWithValue("clave", usuario.Clave);
                command.Parameters.AddWithValue("email", usuario.Email);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public static void EliminarUsuario(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Usuario WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
