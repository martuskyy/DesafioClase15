using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionEntities
{
    public class Producto
    {
        private int _id;
        private string _descripciones;
        private double _costo;
        private double _precioVenta;
        private int _stock;
        private int _idUsuario;

        public Producto()
        {
        }

        public Producto(int id, string descripciones, double costo, double precioVenta, int stock, int idUsuario)
        {
            _id = id;
            _descripciones = descripciones;
            _costo = costo;
            _precioVenta = precioVenta;
            _stock = stock;
            _idUsuario = idUsuario;
        }

        public int Id { get { return _id; } set { _id = value; } }
        public string Descripciones { get { return _descripciones; } set { _descripciones = value; } }
        public double Costo { get { return _costo; } set { _costo = value; } }
        public double PrecioVenta { get { return _precioVenta; } set { _precioVenta = value; } }
        public int Stock { get { return _stock; } set { _stock = value; } }
        public int IdUsuario { get { return _idUsuario; } set { _idUsuario = value; } }
    }
}
