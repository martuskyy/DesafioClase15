using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SistemaGestionEntities;

namespace SistemaGestionData
{
    public class ProductoVendidoData
    {
        private static string connectionString;

        static ProductoVendidoData()
        {
            ProductoVendidoData.connectionString = "Server=.; Database=coderhouse; Trusted_Connection=True;";
        }

        public static List<ProductoVendido> ListarProductosVendidos()
        {
            try
            {
                List<ProductoVendido> listadoDeProductosVendidos = new List<ProductoVendido>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM ProductoVendido;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        ProductoVendido productoVendido = new ProductoVendido();
                        productoVendido.Id = Convert.ToInt32(reader["ID"]);
                        productoVendido.IdProducto = Convert.ToInt32(reader["IdProducto"]);
                        productoVendido.Stock = Convert.ToInt32(reader["Stock"]);
                        productoVendido.IdVenta = Convert.ToInt32(reader["IdVenta"]);

                        listadoDeProductosVendidos.Add(productoVendido);

                    }
                }
                return listadoDeProductosVendidos;
            }

            catch (Exception ex)
            {
                throw new Exception("Error al listar los productos vendidos", ex);
            }
        }

        public static List<ProductoVendido> ListarProductosVendidosPorIDVenta(int idVenta)
        {
            try
            {
                List<ProductoVendido> productosVendidos = new List<ProductoVendido>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM ProductoVendido WHERE IdVenta = @idVenta;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@idVenta", idVenta);
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        ProductoVendido productoVendido = new ProductoVendido();
                        productoVendido.Id = Convert.ToInt32(reader["ID"]);
                        productoVendido.IdProducto = Convert.ToInt32(reader["IdProducto"]);
                        productoVendido.Stock = Convert.ToInt32(reader["Stock"]);
                        productoVendido.IdVenta = Convert.ToInt32(reader["IdVenta"]);

                        productosVendidos.Add(productoVendido);
                    }
                }
                return productosVendidos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los productos vendidos por ID de venta", ex);
            }
        }

        public static ProductoVendido ObtenerProductosVendidos(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ProductoVendido WHERE id=@id;";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("Id", id);

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    ProductoVendido productoVendido = new ProductoVendido();

                    productoVendido.Id = Convert.ToInt32(reader["ID"]);
                    productoVendido.IdProducto = Convert.ToInt32(reader["IdProducto"]);
                    productoVendido.Stock = Convert.ToInt32(reader["Stock"]);
                    productoVendido.IdVenta = Convert.ToInt32(reader["IdVenta"]);

                    return productoVendido;
                }
            }
            throw new Exception("Producto vendido inexistente");
        }

        public static void CrearProductoVendido(ProductoVendido productoVendido)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO ProductoVendido (IdProducto, Stock, IdVenta) values (@idProducto,@stock,@idVenta); select @@IDENTITY as ID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("idProducto", productoVendido.IdProducto);
                command.Parameters.AddWithValue("stock", productoVendido.Stock);
                command.Parameters.AddWithValue("idVenta", productoVendido.IdVenta);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static void ModificarProductosVendidos(int id, ProductoVendido productoVendido)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE ProductoVendido SET IdProducto=@idProducto, Stock=@stock, IdVenta=@idVenta WHERE id = @id ";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("idProducto", productoVendido.IdProducto);
                command.Parameters.AddWithValue("stock", productoVendido.Stock);
                command.Parameters.AddWithValue("idVenta", productoVendido.IdVenta);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public static void EliminarProductoVendido(int idProducto)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM ProductoVendido WHERE idProducto = @idProducto";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("idProducto", idProducto);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }        
    }
}
