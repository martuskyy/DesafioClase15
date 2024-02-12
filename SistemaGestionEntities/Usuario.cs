namespace SistemaGestionEntities
{
    public class Usuario
    {
        private int _id;
        private string _nombre;
        private string _apellido;
        private string _nombreUsuario;
        private string _clave;
        private string _Email;

        public Usuario()
        {
        }

        public Usuario(int id, string nombre, string apellido, string nombreUsuario, string clave, string Email)
        {
            _id = id;
            _nombre = nombre;
            _apellido = apellido;
            _nombreUsuario = nombreUsuario;
            _clave = clave;
            _Email = Email;
        }

        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Apellido { get { return _apellido; } set { _apellido = value; } }
        public string NombreUsuario { get { return _nombreUsuario; } set { _nombreUsuario = value; } }
        public string Clave { get { return _clave; } set { _clave = value; } }
        public string Email { get { return _Email; } set { _Email = value; } }
    }
}
