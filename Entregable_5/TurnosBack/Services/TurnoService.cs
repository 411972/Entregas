using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnosBack.Data.Contracts;
using TurnosBack.Data.Models;

namespace TurnosBack.Services
{
    public class TurnoService : ITurnoService
    {
        private readonly ITurnoRepository _turnoRepository;

        public TurnoService(ITurnoRepository repo)
        {
            _turnoRepository = repo;
        }
        public List<TTurno> ConsultarTurnos(string cliente)
        {
            return _turnoRepository.ConsultarTurnos(cliente);
        }

        public bool CrearTurno(TTurno turno)
        {
            return _turnoRepository.CrearTurno(turno);
        }

        public bool EditarTurno(TTurno turno)
        {
            return _turnoRepository.EditarTurno(turno);
        }

        public bool EliminarTurno(int id)
        {
            return _turnoRepository.EliminarTurno(id);
        }
    }
}
