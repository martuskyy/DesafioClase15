using SistemaGestionEntities;
using SistemaGestionData;
namespace SistemaGestionBussiness
{
    public static class ProductoVendidoBussiness
    {
        public static List<ProductoVendido> ListarProductosVendidos()
        {
            return ProductoVendidoData.ListarProductosVendidos();
        }

        public static ProductoVendido ObtenerProductosVendidos(int id)
        {
            return ProductoVendidoData.ObtenerProductosVendidos(id);
        }
    }
}
