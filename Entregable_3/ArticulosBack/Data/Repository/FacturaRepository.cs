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

        public bool CrearFactura(Factura factura)
        {
            bool result = true;
            SqlTransaction t = null;

            try
            {
                t = DataHelper.GetInstance().beginTransaction();
                List<SqlParameter> lstMaestro = new List<SqlParameter>();
                SqlParameter cliente = new SqlParameter("@cliente", factura.Cliente);
                SqlParameter tipoPago = new SqlParameter("@formaPago", factura.FormaPago.Codigo);
                lstMaestro.Add(cliente);
                lstMaestro.Add(tipoPago);
                int idFactura = DataHelper.GetInstance().ExecuteSPwOutputP("SP_INSERTAR_MAESTRO", lstMaestro, t);

                List<SqlParameter> lstDetalle = new List<SqlParameter>();
                int conteoId = 1;
                bool rows;
                foreach (DetalleFactura df in factura.Detalles)
                {
                    SqlParameter idDetalle = new SqlParameter("@id_detalle", conteoId);
                    SqlParameter idFac = new SqlParameter("@id_factura", idFactura);
                    SqlParameter idArt = new SqlParameter("@id_articulo", df.Articulo.Codigo);
                    SqlParameter cantidad = new SqlParameter("@cantidad", df.Cantidad);

                    lstDetalle.Add(idDetalle);
                    lstDetalle.Add(idFac);
                    lstDetalle.Add(idArt);
                    lstDetalle.Add(cantidad);
                    rows = DataHelper.GetInstance().ExecuteSPPost("SP_INSERTAR_DETALLE", lstDetalle, t);
                    lstDetalle.Clear();
                    conteoId++;
                }



                DataHelper.GetInstance().CommitTransaction(t);

            }
            catch (Exception)
            {
                result = false;
                DataHelper.GetInstance().RollbackTransaction(t);
            }
            finally
            {
                DataHelper.GetInstance().CloseConnection();
            }


            return result;
        }

        public bool EditarFactura(Factura factura)
        {
            bool result = true;
            SqlTransaction t = null;
            int rows = 0;
            int rowsDetalle;
            List<SqlParameter> lstP = new List<SqlParameter>();

            try
            {
                t = DataHelper.GetInstance().beginTransaction();

                SqlParameter idFactura = new SqlParameter("@id_factura", factura.NroFactura);
                lstP.Add(idFactura);
                rows = DataHelper.GetInstance().ExecuteSPwOutputP("SP_ACTUALIZAR_FACTURA", lstP, t);

                List<SqlParameter> lstDetalle = new List<SqlParameter>();
                int conteoId = 1;

                foreach (DetalleFactura df in factura.Detalles)
                {
                    SqlParameter idDetalle = new SqlParameter("@id_detalle", conteoId);
                    SqlParameter idFac = new SqlParameter("@id_factura", factura.NroFactura);
                    SqlParameter idArt = new SqlParameter("@id_articulo", df.Articulo.Codigo);
                    SqlParameter cantidad = new SqlParameter("@cantidad", df.Cantidad);

                    lstDetalle.Add(idDetalle);
                    lstDetalle.Add(idFac);
                    lstDetalle.Add(idArt);
                    lstDetalle.Add(cantidad);
                    DataHelper.GetInstance().ExecuteSPPost("SP_INSERTAR_DETALLE", lstDetalle, t);
                    lstDetalle.Clear();
                    conteoId++;
                }

                DataHelper.GetInstance().CommitTransaction(t);


            }
            catch (Exception exc)
            {
                result = false; 
                DataHelper.GetInstance().RollbackTransaction(t);
                throw exc;
            }
            finally
            {
                DataHelper.GetInstance().CloseConnection();
            }

            return result;
        }
    }
}
