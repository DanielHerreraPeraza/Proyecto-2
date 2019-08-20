using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAcess.Dao
{
    public class SqlDao
    {   
                     
       private string CONNECTION_STRING = "";
       
       private static SqlDao instance;

        private SqlDao()
        {
            CONNECTION_STRING=ConfigurationManager.ConnectionStrings["CONN_STRING"].ConnectionString;
        }

        public static SqlDao GetInstance()
        {
            if (instance == null)
                instance = new SqlDao();

            return instance;
        }

       public void ExecuteProcedure(SqlOperation sqlOperation)
       {
            using (var conn = new SqlConnection(CONNECTION_STRING))
            using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {                          
                foreach (var param in sqlOperation.Parameters)
                {
                    command.Parameters.Add(param);
                }

                try
                {
                    conn.Open();
                }
                catch(Exception ex)
                {
                    throw new SystemException("El servidor no se encuentra disponible en estos momentos, por favor intente más tarde");
                }

                command.ExecuteNonQuery();
            }
       }

       public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation sqlOperation)
        {
            var lstResult=new List<Dictionary<string,object>>();

            using (var conn = new SqlConnection(CONNECTION_STRING))
            using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            { 
                foreach (var param in sqlOperation.Parameters)
                {
                    command.Parameters.Add(param);
                }

                try
                {
                    conn.Open();

                }
                catch (Exception ex)
                {
                    throw new SystemException("El servidor no se encuentra disponible en estos momentos, por favor intente más tarde");
                }

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var dict = new Dictionary<string, object>();
                        for (var lp = 0; lp < reader.FieldCount; lp++)
                        {
                            dict.Add(reader.GetName(lp), reader.GetValue(lp));
                        }
                        lstResult.Add(dict);
                    }
                }
            }

            return lstResult;
        }      
    }
}
