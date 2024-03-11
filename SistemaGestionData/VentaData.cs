using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SistemaGestionEntities;

namespace SistemaGestionData
{
    public class VentaData
    {
        private static string connectionString;

        static VentaData()
        {
            VentaData.connectionString = "Server=.; Database=master; Trusted_Connection=True;";
        }

        public static List<Venta> ListarVentas()
        {
            try
            {
                List<Venta> listadoDeVentas = new List<Venta>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Venta;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Venta venta = new Venta();
                        venta.Id = Convert.ToInt32(reader["ID"]);
                        venta.Comentarios = reader["Comentarios"].ToString();
                        venta.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);

                        listadoDeVentas.Add(venta);

                    }
                }
                return listadoDeVentas;
            }

            catch (Exception ex)
            {
                throw new Exception("Error al listar las ventas", ex);
            }
        }

        public static Venta ObtenerVentas(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Venta WHERE id=@id;";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("Id", id);

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Venta venta = new Venta();
                    venta.Id = Convert.ToInt32(reader["ID"]);
                    venta.Comentarios = reader["Comentarios"].ToString();
                    venta.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);

                    return venta;
                }
            }
            throw new Exception("Venta inexistente");
        }

        public static void CrearVenta(Venta venta)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Venta (Comentarios, IdUsuario) values (@comentarios, @idUsuario); select @@IDENTITY as ID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("comentarios", venta.Comentarios);
                command.Parameters.AddWithValue("idUsuario", venta.IdUsuario);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static void ModificarVentas(int id, Venta venta)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Venta SET Comentario=@comentario, IdUsuario=@idUsuario WHERE id = @id ";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("comentario", venta.Comentarios);
                command.Parameters.AddWithValue("idUsuario", venta.IdUsuario);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static void EliminarVenta(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Venta WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
