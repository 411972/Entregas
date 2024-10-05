using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnosBack.Data.Contracts;
using TurnosBack.Data.Models;

namespace TurnosBack.Services
{
    public class ServicesService : IServicesService
    {
        private IServicesRepository _servicesRepository;

        public ServicesService(IServicesRepository servicesRepository)
        {
            _servicesRepository = servicesRepository;
        }
        public bool Create(TServicio servicio)
        {
            return _servicesRepository.Create(servicio);
        }

        public bool Delete(int id)
        {
            return _servicesRepository.Delete(id);
        }

        public TServicio Get(int precio_min, int precio_max)
        {
            return _servicesRepository.Get(precio_min, precio_max);
        }

        public List<TServicio> GetAll()
        {
            return _servicesRepository.GetAll();
        }
        
        public TServicio GetById(int id)
        {
            return _servicesRepository.GetById(id);
        }

        public bool UpdateService(int id,TServicio servicio)
        {
            return _servicesRepository.UpdateService(id,servicio);
        }
    }
}
