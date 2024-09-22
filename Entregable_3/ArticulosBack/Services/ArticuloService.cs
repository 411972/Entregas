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
    public class ArticuloService
    {
        private IAplicacion _repository;

        public ArticuloService()
        {
            _repository = new ArticuloRepository();
        }

        public List<Articulo> ObtenerArticulos(string? nombre)
        {
            return _repository.ObtenerArticulos(nombre);
        }

        public bool AgregarArticulo(Articulo a)
        {
            return _repository.AgregarArticulo(a);
        }

        public bool EditarArticulo(Articulo a)
        {
            return _repository.EditarArticulo(a);
        }

        public bool EliminarArticulo(int id)
        {
            return _repository.EliminarArticulo(id);
        }
    }
}
