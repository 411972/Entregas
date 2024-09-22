using ArticulosBack.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticulosBack.Data
{
    public class DataHelper
    {
        private SqlConnection _connection;
        private static DataHelper _instance;

        private DataHelper(){

            _connection = new SqlConnection("Data Source=FRANCO\\SQLEXPRESS;Initial Catalog=facturaciones;Integrated Security=True;");
        }

        public static DataHelper GetInstance()
        {
            if(_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }

        public SqlConnection OpenConnection()
        {
            if(_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            return _connection;
        }

        public SqlConnection CloseConnection()
        {
            if(_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
            return _connection ;
        }


        public void CommitTransaction(SqlTransaction transaction)
        {
            transaction?.Commit();
        }

        public void RollbackTransaction(SqlTransaction transaction)
        {
            transaction?.Rollback();
        }
        public SqlTransaction beginTransaction()
        {

            return OpenConnection().BeginTransaction();
        }

        public DataTable ExecuteSPGet(string sp, List<SqlParameter>? lstP)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(sp, _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if(lstP != null)
                {
                    foreach(SqlParameter p in lstP)
                    {
                        cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                    }
                }

                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception exc)
            {

                throw exc;
            }

            return dt;

        }
        
        public bool ExecuteSPPost(string sp, List<SqlParameter> lstP, SqlTransaction t)
        {
            bool result = false;
            int rows = 0;
            SqlCommand cmd = new SqlCommand(sp, _connection, t);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                foreach( SqlParameter p in lstP)
                {
                    cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                }

                rows = cmd.ExecuteNonQuery();

                if(rows > 0)
                {
                    result = true;
                }

            }
            catch (Exception exc)
            {

                throw exc;
            }

            return result;
        }

        public int ExecuteSPwOutputP(string sp, List<SqlParameter> paramsql, SqlTransaction t)
        {
            int idFactura = 0;
            int rows = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(sp, _connection, t);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in paramsql)
                {
                    cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                }

                if (sp == "SP_INSERTAR_MAESTRO")
                {
                    SqlParameter paramOutput = new SqlParameter("@id_factura", SqlDbType.Int); 
                    paramOutput.Direction = ParameterDirection.Output; 
                    cmd.Parameters.Add(paramOutput); 

                    cmd.ExecuteNonQuery(); 
                    idFactura = (int)paramOutput.Value; 
                    cmd.Parameters.Clear();
                }
                else
                {
                    rows = cmd.ExecuteNonQuery();
                }


            }
            catch (Exception exc)
            {
                throw exc;
            }

            if (sp == "SP_INSERTAR_MAESTRO")
            {
                return idFactura;
            }
            else
            {
                return rows;
            }

        }

    }
}
