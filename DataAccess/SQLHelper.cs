﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SQLHelper
    {
        static string strConnection;
        public SQLHelper()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
        }

        public static string connectionstring()
        {
            //  strConnection = @"Data Source=DESKTOP-DHNUDGQ\SQLEXPRESS2017;Initial Catalog=TECH;Persist Security Info=True;User ID=sa;password = pass1234;";
            strConnection = "Data Source=DESKTOP-6JRBTHB\\SQLEXPRESS01;Initial Catalog=TECH;Integrated Security=True";
            return strConnection;
        }


        #region Create Command

        public static SqlCommand CreateCommand(string sql, CommandType type, List<SqlParameter> param)
        {
            connectionstring();
            SqlConnection con = new SqlConnection(strConnection);
            //con.ConnectionString = strConnection;
            SqlCommand cmd = new SqlCommand();
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = type;
            cmd.Connection = con;
            if (param.Count > 0)
            {
                foreach (SqlParameter p in param)
                {
                    if (p != null)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
            }

            return cmd;
        }
        public static SqlCommand CreateCommand(string sql, CommandType type)
        {
            connectionstring();
            SqlConnection con = new SqlConnection(strConnection);
            //con.ConnectionString = "";

            SqlCommand cmd = new SqlCommand();
            cmd = con.CreateCommand();
            cmd.CommandType = type;
            cmd.CommandText = sql;
            cmd.Connection = con;
            return cmd;
        }

        #endregion
        #region ExecuteNonQuery
        public static void ExecuteNonQuery(string sql, CommandType type, List<SqlParameter> paramList)
        {

            SqlCommand cmd = new SqlCommand();
            cmd = CreateCommand(sql, type, paramList);
            cmd.CommandType = type;
            cmd.CommandText = sql;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public static void ExecuteNonQuery(string sql, CommandType type)
        {

            SqlCommand cmd = new SqlCommand();
            cmd = CreateCommand(sql, type);
            cmd.CommandType = type;
            cmd.CommandText = sql;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        //Execute non query new function
        public static void ExecuteNonQuery(SqlCommand cmd)
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        //--------------------------------
        #endregion
        #region Create Parameters
        public static SqlParameter CreateParameter(string paramName, SqlDbType sqlType, int Size)
        {
            SqlParameter paramenter = new SqlParameter(paramName, sqlType, Size);
            return paramenter;
        }
        public static SqlParameter CreateParameter(string paramName, SqlDbType sqlType)
        {
            SqlParameter paramenter = new SqlParameter(paramName, sqlType);
            return paramenter;
        }
        #endregion
        #region FillDataSet
        public static DataSet FillDataSet(string sql, CommandType type)
        {
            SqlDataAdapter sqlDA = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd = CreateCommand(sql, type);
            sqlDA.SelectCommand = cmd;
            sqlDA.Fill(ds);
            cmd.Connection.Close();
            return ds;
        }
        public static DataSet FillDataSet(string sql, CommandType type, List<SqlParameter> paramList)
        {
            SqlDataAdapter sqlDa = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd = CreateCommand(sql, type, paramList);
            cmd.CommandText = sql;
            sqlDa.SelectCommand = cmd;
            sqlDa.Fill(ds);
            cmd.Connection.Close();
            return ds;
        }
        #endregion
        #region ExecuteReader
        public static SqlDataReader ExecuteReader(string sql, CommandType type)
        {
            SqlDataReader dataReader;
            SqlCommand cmd = new SqlCommand();

            cmd = CreateCommand(sql, type);
            cmd.Connection.Open();
            dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);


            return dataReader;
        }
        public static SqlDataReader ExecuteReader(string sql, CommandType type, List<SqlParameter> paramList)
        {
            SqlDataReader dataReader;
            SqlCommand cmd = new SqlCommand();
            cmd = CreateCommand(sql, type, paramList);
            cmd.Connection.Open();
            dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            return dataReader;
        }
        #endregion

    }
}
