using SistemaGestionEntities;
using SistemaGestionData;
namespace SistemaGestionBussiness
{
    public static class ProductoBussiness
    {
        public static List<Producto> ListarProductos()
        {
            return ProductoData.ListarProductos();
        }

        public static Producto ObtenerProductos(int id)
        {
            return ProductoData.ObtenerProductos(id);
        }
    }
}