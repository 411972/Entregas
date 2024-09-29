using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnosBack.Data.Contracts;
using TurnosBack.Data.Models;

namespace TurnosBack.Data.Repositories
{
    public class ServicesRepository : IServicesRepository
    {
        private TurnosDbContext _context;

        public ServicesRepository(TurnosDbContext context)
        {
            _context = context;
        }
        public bool Create(TServicio servicio)
        {
            bool result = false;

            if(servicio != null)
            {
                try
                {
                    _context.TServicios.Add(servicio);
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

        public bool Delete(int id)
        {
            bool result = false;
            var servicioEncontrado = _context.TServicios.FirstOrDefault(e => e.Id == id);

            if(servicioEncontrado != null)
            {
                
                servicioEncontrado.Activo = false;
                try
                {
                    _context.TServicios.Update(servicioEncontrado);
                    _context.SaveChanges();
                    result = true;
                }
                catch (Exception exc )
                {

                    throw exc;
                }

            }
            return result;
        }

        public TServicio Get(int precio_min, int precio_max)
        {
            return _context.TServicios
                    .Where( e => e.Costo >= precio_min && e.Costo <= precio_max)
                    .FirstOrDefault();
        }

        public List<TServicio> GetAll()
        {
            return _context.TServicios.ToList();
        }

        public TServicio GetById(int id)
        {
            return _context.TServicios.FirstOrDefault(e => e.Id == id);
        }

        public bool UpdateService(int id, TServicio servicio)
        {
            var servicioExistente = GetById(id);
            bool result;

            if (servicioExistente == null)
            {
                result =false;
            }
            else
            {
                try
                {
                    servicioExistente.Nombre = servicio.Nombre;
                    servicioExistente.Costo = servicio.Costo;
                    servicioExistente.EnPromocion = servicio.EnPromocion;

                    _context.SaveChanges();
                    result = true;
                }
                catch (Exception exc)
                {

                    throw exc;
                };
            }

            return result;

        }
    }
}
