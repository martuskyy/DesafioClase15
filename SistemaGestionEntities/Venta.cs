using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionEntities
{
    public class Venta
    {
        private int _id;
        private string _comentario;
        private int _idUsuario;

        public Venta()
        {
        }

        public Venta(int id, string comentario, int idUsuario)
        {
            _id = id;
            _comentario = comentario;
            _idUsuario = idUsuario;
        }

        public int Id { get { return _id; } set { _id = value; } }
        public string Comentario { get { return _comentario; } set { _comentario = value; } }
        public int IdUsuario { get { return _idUsuario; } set { _idUsuario = value; } }
    }
}
