using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ProductCRUD.Data
{
    public class Connection
    {
        protected SqlConnection _connection;

        protected void Connect()
        {
            string connectionStr = ConfigurationManager.ConnectionStrings["db_products"].ConnectionString;
            
            try
            {
                _connection = new SqlConnection(connectionStr);
                _connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void Disconnect()
        {
            try
            {
                _connection.Close();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool Command(string query, List<Param> paramList = null)
        {
            Connect();

            try
            {
                SqlCommand cmd = new SqlCommand(query, _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (paramList != null)
                {
                    foreach (Param param in paramList)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return false;
            }
            finally
            {
                Disconnect();
            }
        }

        public DataSet Query(string query, List<Param> paramList = null)
        {
            Connect();

            try
            {
                SqlCommand cmd = new SqlCommand(query, _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (paramList != null)
                {
                    foreach (Param param in paramList)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);

                return ds;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
            finally
            {
                Disconnect();
            }
        }
    }
}