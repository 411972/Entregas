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
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TServicio Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<TServicio> GetAll()
        {
            return _context.TServicios.ToList();
        }

        public bool Update(TServicio servicio)
        {
            throw new NotImplementedException();
        }
    }
}
