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
    internal class ArticuloRepository : IAplicacion
    {
        public bool AgregarArticulo(Articulo? a)
        {
            bool result = true;
            SqlTransaction t = null;

            try
            {
                t = DataHelper.GetInstance().beginTransaction();
                List<SqlParameter> parametrosLst = new List<SqlParameter>();
                SqlParameter nombreArt = new SqlParameter("@articulo", a.Nombre);
                SqlParameter precioArt = new SqlParameter("@precio", a.precioUnitario);

                parametrosLst.Add(nombreArt);
                parametrosLst.Add(precioArt);

                result = DataHelper.GetInstance().ExecuteSPPost("SP_AGREGAR_ARTICULO", parametrosLst, t);


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

        public bool EditarArticulo(Articulo? a)
        {
            bool result = true;
            SqlTransaction t = null;

            try
            {
                t = DataHelper.GetInstance().beginTransaction();
                List<SqlParameter> parametrosLst = new List<SqlParameter>();
                SqlParameter codArt = new SqlParameter("@id", a.Codigo);
                SqlParameter nombreArt = new SqlParameter("@articulo", a.Nombre);
                SqlParameter precioArt = new SqlParameter("@precio", a.precioUnitario);

                parametrosLst.Add(codArt);
                parametrosLst.Add(nombreArt);
                parametrosLst.Add(precioArt);

                result = DataHelper.GetInstance().ExecuteSPPost("SP_EDITAR_ARTICULO", parametrosLst, t);


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


            return result; ;
        }

        public bool EliminarArticulo(int id)
        {
            bool result = true;
            SqlTransaction t = null;

            try
            {
               t = DataHelper.GetInstance().beginTransaction();
               List<SqlParameter> parametrosLst = new List<SqlParameter>();
               SqlParameter codArt = new SqlParameter("@id", id);
                parametrosLst.Add(codArt) ;

                result = DataHelper.GetInstance().ExecuteSPPost("SP_ELIMINAR_ARTICULO", parametrosLst, t);

                DataHelper.GetInstance().CommitTransaction(t);

            }
            catch (Exception exc)
            {
                result = false;
                DataHelper.GetInstance().RollbackTransaction(t);
                throw exc;
            }

            return result;
        }

        public List<Articulo> ObtenerArticulos(string? nombre)
        {
            List<SqlParameter> listP = new List<SqlParameter>();
            List<Articulo> listA = new List<Articulo>();
            DataTable list;
            try
            {
                DataHelper.GetInstance().OpenConnection();
                if (nombre != null)
                {
                    SqlParameter p = new SqlParameter("@nombre_articulo", nombre);
                    listP.Add(p);
                    list = DataHelper.GetInstance().ExecuteSPGet("SP_OBTENER_ARTICULOS", listP);
                }

                list = DataHelper.GetInstance().ExecuteSPGet("SP_OBTENER_ARTICULOS", listP);

                foreach (DataRow r in list.Rows)
                {
                    Articulo oArticulo = new Articulo();
                    oArticulo.Codigo = (int)r["id_articulo"];
                    oArticulo.Nombre = (string)r["articulo"];
                    oArticulo.precioUnitario = Convert.ToDouble(r["precioUnitario"]);

                    listA.Add(oArticulo);
                }


            }
            catch (Exception exc)
            {

                throw exc;
            }

            return listA;

        }


    }
}
