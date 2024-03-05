using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SistemaGestionEntities;

namespace SistemaGestionData
{
    public static class ProductoData
    {
        private static string connectionString;

        static ProductoData()
        {
            ProductoData.connectionString = "Server=.; Database=master; Trusted_Connection=True;";
        }

        public static List<Producto> ListarProductos()
        {
            try
            {
                List<Producto> listadoDeProductos = new List<Producto>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Producto;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Producto producto = new Producto();
                        producto.Id = Convert.ToInt32(reader["ID"]);
                        producto.Descripcion = reader["Descripcion"].ToString();
                        producto.Costo = Convert.ToInt64(reader["Costo"]);
                        producto.PrecioVenta = Convert.ToInt64(reader["PrecioVenta"]);
                        producto.Stock = Convert.ToInt32(reader["Stock"]);
                        producto.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);

                        listadoDeProductos.Add(producto);

                    }
                }
                return listadoDeProductos;
            }

            catch (Exception ex)
            {
                throw new Exception("Error al listar los productos", ex);
            }
        }

        public static Producto ObtenerProductos(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Producto WHERE id=@id;";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("Id", id);

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Producto producto = new Producto();
                    producto.Id = Convert.ToInt32(reader["ID"]);
                    producto.Descripcion = reader["Descripcion"].ToString();
                    producto.Costo = Convert.ToInt64(reader["Costo"]);
                    producto.PrecioVenta = Convert.ToInt64(reader["PrecioVenta"]);
                    producto.Stock = Convert.ToInt32(reader["Stock"]);
                    producto.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);


                    return producto;
                }
            }
            throw new Exception("Producto inexistente");
        }

        public static void CrearProducto(Producto producto)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Producto (Descripcion, Costo, PrecioVenta, Stock, IdUsuario)" +
                               "values (@descripcion,@costo,@precioVenta,@stock,@idUsuario); select @@IDENTITY as ID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("costo", producto.Costo);
                command.Parameters.AddWithValue("precioVenta", producto.PrecioVenta);
                command.Parameters.AddWithValue("stock", producto.Stock);
                command.Parameters.AddWithValue("idUsuario", producto.IdUsuario);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static void ModificarProductos(int id, Producto producto)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Producto SET Descripcion=@descripcion, Costo=@costo, PrecioVenta=@precioVenta, Stock=@stock, IdUsuario=@idUsuario" +
                               "WHERE id = @id ";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("costo", producto.Costo);
                command.Parameters.AddWithValue("precioVenta", producto.PrecioVenta);
                command.Parameters.AddWithValue("stock", producto.Stock);
                command.Parameters.AddWithValue("idUsuario", producto.IdUsuario);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public static void EliminarProducto(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Producto WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
