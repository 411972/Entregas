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
            return _servicesRepository.GetAll();
        }

        public bool Update(TServicio servicio)
        {
            throw new NotImplementedException();
        }
    }
}
