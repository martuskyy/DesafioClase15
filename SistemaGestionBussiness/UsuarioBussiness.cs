using SistemaGestionEntities;
using SistemaGestionData;
namespace SistemaGestionBussiness
{
    public static class UsuarioBussiness
    {
        public static List<Usuario> ListarUsuarios()
        {
            return UsuarioData.ListarUsuarios();
        }

        public static Usuario ObtenerUsuarios(int id)
        {
            return UsuarioData.ObtenerUsuarios(id);
        }
    }
}
