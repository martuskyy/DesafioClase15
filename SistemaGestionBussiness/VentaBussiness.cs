using SistemaGestionEntities;
using SistemaGestionData;
using System.Data.SqlClient;
namespace SistemaGestionBussiness
{
    public class VentaBussiness
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
