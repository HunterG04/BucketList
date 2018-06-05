using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BucketList
{
    class Summary
    {
        private int totalTasks;
        private int numberCompleteTasks;
        private int numberIncompleteTasks;
        private DBConnector dbCon;

        Summary(int newTotalTasks, int newNumberCompleteTasks, int newNumberIncompleteTasks, DBConnector newDBCon)
        {
            totalTasks = newTotalTasks;
            numberCompleteTasks = newNumberCompleteTasks;
            numberIncompleteTasks = newNumberIncompleteTasks;
            dbCon = newDBCon;
        }

        public int getTotalTasks()
        {
            return totalTasks;
        }

        public int getTasksCompleted()
        {
            return numberCompleteTasks;
        }

        public int getIncompleteTasks()
        {
            return numberIncompleteTasks;
        }

        public void incrementTotalTasks()
        {
            totalTasks++;
        }

        public void decrementTotalTasks()
        {
            totalTasks--;
        }

        public void incrementCompleteTasks()
        {
            numberCompleteTasks++;
        }

        public void decrementCompleteTasks()
        {
            numberCompleteTasks--;
        }

        public void incrementIncompleteTasks()
        {
            numberIncompleteTasks++;
        }

        public void decrementinCompleteTasks()
        {
            numberIncompleteTasks--;
        }

        private void updateDB(string query)
        {
            dbCon.executeCommand(query);
        }

        public void loadSummaryFromDB()
        {
            string query = "SELECT TOTALTASKS, INCOMPLETETASKS, COMPLETETASKS " +
                           "FROM SUMMARY ";

            DataSet ds = dbCon.queryDB(query);

            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    totalTasks = (int)row["TOTALTASKS"];
                    numberIncompleteTasks = (int)row["INCOMPLETETASKS"];
                    numberCompleteTasks = (int)row["COMPLETETASKS"];
                }
            }
        }
    }
}
