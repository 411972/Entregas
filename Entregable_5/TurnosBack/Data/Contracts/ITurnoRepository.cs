using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnosBack.Data.Models;

namespace TurnosBack.Data.Contracts
{
    public interface ITurnoRepository
    {
        bool CrearTurno(TTurno turno);

        bool EditarTurno(TTurno turno);

        List<TTurno> ConsultarTurnos(string cliente);

        bool EliminarTurno(int id);
    }
}
