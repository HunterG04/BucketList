using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BucketList
{
    public class DBConnector
    {
        private string connectionString;
        private SqlConnection con;

        public DBConnector(string newConString)
        {
            con = new SqlConnection();

            connectionString = newConString;
            con.ConnectionString = connectionString;
        }

        public void setConnectionString(string newConString)
        {
            connectionString = newConString;

            con.ConnectionString = connectionString;
        }

        public void executeCommand(string cmdQuery)
        {
            try
            {
                con.Open();
            }
            catch (Exception e)
            {
                // connection is invalid
                return;
            }

            SqlCommand cmd = new SqlCommand(cmdQuery, con);

            cmd.ExecuteNonQuery();
            con.Close();
        }

        /*
         * Call this function to run a select statement on the sql DB
         * being used (from the connection string)
         */
        public DataSet queryDB(string selectQuery)
        {
            try
            {
                con.Open();
            }
            catch (Exception e)
            {
                // connection is invalid
                return null;
            }

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.CommandText = selectQuery;
            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            con.Close();

            return ds;
        }
    }
}
