using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnosBack.Data.Contracts;
using TurnosBack.Data.Models;

namespace TurnosBack.Data.Repositories
{
    public class TurnoRepository : ITurnoRepository
    {
        private readonly TurnosDbContext _context;

        public TurnoRepository(TurnosDbContext context)
        {
            _context = context; 
        }
        public List<TTurno> ConsultarTurnos(string cliente)
        {
            return _context.TTurnos
                    .Where(t => t.Cliente == cliente)
                    .ToList();
        }

        public bool CrearTurno(TTurno turno)
        {
            bool result = false;
            try
            {
                _context.TTurnos.Add(turno);
                _context.SaveChanges();
                result = true;

            }
            catch (Exception exc)
            {

                throw exc;
            }

            return result;
             
        }

        public bool EditarTurno(TTurno turno)
        {
            bool result = false;
            var turn = _context.TTurnos.FirstOrDefault(t => t.Id == turno.Id);
            if(turn != null)
            {
                try
                {
                    turn.Cliente = turno.Cliente;
                    turn.MotivoCancelacion = turno.MotivoCancelacion;
                    turn.FechaCancelacion = turno.FechaCancelacion;
                    turn.Fecha = turno.Fecha;
                    turn.Hora = turno.Hora;
                    _context.TTurnos.Update(turn);
                    _context.SaveChanges();
                    result = true;
                }
                catch (Exception exc)
                {

                    throw exc;
                }
            }

            return result;
        }

        public bool EliminarTurno(int id)
        {
            bool result = false;
            var turn = _context.TTurnos.FirstOrDefault(t => t.Id == id);
            if (turn != null)
            {
                try
                {
                    turn.FechaCancelacion = DateTime.Now;
                    _context.TTurnos.Update(turn);
                    _context.SaveChanges();
                    result = true;
                }
                catch (Exception exc)
                {

                    throw exc;
                }
            }

            return result;
        }
    }
}
