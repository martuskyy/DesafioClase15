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
            VentaData.connectionString = "Server=.; Database=coderhouse; Trusted_Connection=True;";
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
                        venta.Comentario = reader["Comentario"].ToString();
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
                    venta.Comentario = reader["Comentario"].ToString();
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
                string query = "INSERT INTO Venta (Comentario, IdUsuario) values (@comentario, @idUsuario); select @@IDENTITY as ID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("comentario", venta.Comentario);
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
                command.Parameters.AddWithValue("comentario", venta.Comentario);
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

        public bool AgregarNuevaVenta(int idUsuario, List<Producto> productos)
        {
            string comentarios = string.Join("-", productos.Select(p => p.Descripcion));

            int ventaId;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Venta (Comentario, IdUsuario) VALUES (@comentario, @idUsuario); SELECT SCOPE_IDENTITY();";
                    command.Parameters.AddWithValue("@comentario", comentarios);
                    command.Parameters.AddWithValue("@idUsuario", idUsuario);
                    ventaId = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            MarcarComoVendidos(productos, ventaId);
            RestarStockVendidos(productos);

            return true;
        }

        private void MarcarComoVendidos(List<Producto> productos, int idVenta)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (var producto in productos)
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO ProductoVendido (IdProducto, IdVenta, Stock) VALUES (@idProducto, @idVenta, @stock)";
                        command.Parameters.AddWithValue("@idProducto", producto.Id);
                        command.Parameters.AddWithValue("@idVenta", idVenta);
                        command.Parameters.AddWithValue("@stock", producto.Stock);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void RestarStockVendidos(List<Producto> productos)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (var producto in productos)
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE Producto SET Stock -= @stock WHERE Id = @idProducto";
                        command.Parameters.AddWithValue("@stock", producto.Stock);
                        command.Parameters.AddWithValue("@idProducto", producto.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void SumarStockVendidos(List<Producto> productos)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (var producto in productos)
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE Producto SET Stock += @stock WHERE Id = @idProducto";
                        command.Parameters.AddWithValue("@stock", producto.Stock);
                        command.Parameters.AddWithValue("@idProducto", producto.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
