using SistemaGestionEntities;
using SistemaGestionData;
namespace SistemaGestionBussiness
{
    public static class VentaBussiness
    {
        public static List<Venta> ListarVentas()
        {
            return VentaData.ListarVentas();
        }

        public static Venta ObtenerVentas(int id)
        {
            return VentaData.ObtenerVentas(id);
        }
    }
}
