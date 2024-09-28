using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnosBack.Data.Models;

namespace TurnosBack.Data.Contracts
{
    public interface IServicesRepository
    {
        List<TServicio>  GetAll();
       
        TServicio Get(int id);
        
        bool Create(TServicio servicio);

        bool Update(TServicio servicio);

        bool Delete(int id);


    }
}
