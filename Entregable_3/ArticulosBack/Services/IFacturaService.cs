using ArticulosBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticulosBack.Services
{
    public interface IFacturaService
    {
        public List<Factura> ConsultarFacturas(DateTime? f, int? fp);

        public bool CrearFactura(Factura f);

        public bool EditarFactura(Factura f);
    }
}
