using ArticulosBack.Data.Contracts;
using ArticulosBack.Data.Repository;
using ArticulosBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticulosBack.Services
{
    public class FacturaService : IFacturaService
    {
        private IFacturaRepository _repository;

        public FacturaService()
        {
            _repository = new FacturaRepository();  
        }
        public List<Factura> ConsultarFacturas(DateTime? f, int? fp)
        {
            return _repository.ConsultarFacturas(f, fp);
        }


        public bool CrearFactura(Factura f)
        {
            return _repository.CrearFactura(f);
        }

        public bool EditarFactura(Factura f)
        {
            return _repository.EditarFactura(f);
        }
    }
}
