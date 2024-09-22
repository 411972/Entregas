using ArticulosBack.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticulosBack.Data.Contracts
{
    public interface IAplicacion
    {
        List<Articulo> ObtenerArticulos(string? nombre);

        bool AgregarArticulo(Articulo? a);

        bool EditarArticulo(Articulo? a);

        bool EliminarArticulo(int id);
    }
}
