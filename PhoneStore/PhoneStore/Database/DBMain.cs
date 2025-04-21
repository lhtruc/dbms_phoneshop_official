﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Database
{
    public class DBMain
    {
        string ConnStr = "Data Source=htruc;Initial Catalog=PHONESHOP;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataAdapter adapter = null;

        public DBMain()
        {
            conn = new SqlConnection(ConnStr);
            cmd = conn.CreateCommand();
        }

        public DataSet ExecuteQuerryDataSet(string strSQL, CommandType ct)
        {
            // Check the connection: If it's already opened -> close -> reopen
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            // cmd = input SQL string, type = command type
            cmd.CommandText = strSQL;
            cmd.CommandType = ct;

            // Get the data
            adapter = new SqlDataAdapter(cmd);
            // Creata a dataset to save data
            DataSet ds = new DataSet();
            // Fill data
            adapter.Fill(ds);
            // Return
            return ds;
        }

        public bool MyExecutedNoneQuerry(string strSQL, CommandType ct, ref string error)
        {
            bool f = false;
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();

                cmd.CommandText = strSQL;
                cmd.CommandType = ct;

                try
                {
                    cmd.ExecuteNonQuery();
                    f = true;
                }
                catch (SqlException ex)
                {
                    error = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return f;
        }

        public bool MyExecutedNoneQuerry(string strSQL, CommandType ct, SqlParameter[] parameters, ref string error)
        {
            bool f = false;
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(strSQL, conn))
                {
                    cmd.CommandType = ct;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    cmd.ExecuteNonQuery();
                    f = true;
                }
            }
            catch (SqlException ex)
            {
                error = ex.Message;
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return f;
        }


        public DataSet ExecuteQuerryDataSet(string strSQL, CommandType ct, SqlParameter[] parameters)
        {
            // Check the connection: If it's already opened -> close -> reopen
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            // cmd = input SQL string, type = command type
            cmd.CommandText = strSQL;
            cmd.CommandType = ct;

            // Add parameters to the command if any
            if (parameters != null)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(parameters);
            }

            // Get the data
            adapter = new SqlDataAdapter(cmd);
            // Create a dataset to save data
            DataSet ds = new DataSet();
            // Fill data
            adapter.Fill(ds);
            // Return
            return ds;
        }
        public object ExecuteScalar(string sql, CommandType commandType, SqlParameter[] parameters)
        {
            object result = null;

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = commandType;
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    result = cmd.ExecuteScalar();
                }
            }

            return result;
        }
    }
}

