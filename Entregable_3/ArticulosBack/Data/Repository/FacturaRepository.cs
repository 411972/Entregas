using ArticulosBack.Data.Contracts;
using ArticulosBack.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticulosBack.Data.Repository
{
    public class FacturaRepository : IFacturaRepository
    {
        public List<Factura> ConsultarFacturas(DateTime? f, int? fp)
        {
            List<SqlParameter> lstP = new List<SqlParameter>();
            DataTable dt = new DataTable();
            List < Factura > listFact = new List<Factura>();    
                lstP.Add(new SqlParameter("@fecha", f));
                lstP.Add(new SqlParameter("@tipo_pago", fp));



            try
            {
                DataHelper.GetInstance().OpenConnection();
                dt = DataHelper.GetInstance().ExecuteSPGet("SP_OBTENER_FACTURAS_FILTRO", lstP);
                foreach (DataRow r in dt.Rows)
                {
                    Factura oFactura = new Factura();
                    oFactura.NroFactura = (int)r["id_factura"];
                    oFactura.Fecha = (DateTime)r["fecha"];
                    FormaPago oFP = new FormaPago();
                    oFP.Nombre = (string)r["forma_pago"];
                    oFactura.FormaPago = oFP;

                    listFact.Add(oFactura);
                }

                DataHelper.GetInstance().CloseConnection();
            }
            catch (Exception exc)
            {
                DataHelper.GetInstance().CloseConnection();
                throw exc;
            }

            return listFact;

        }

        public bool CrearFactura(Factura f)
        {
            throw new NotImplementedException();
        }

        public bool EditarFactura(Factura f)
        {
            throw new NotImplementedException();
        }
    }
}
