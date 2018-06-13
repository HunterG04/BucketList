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
    public class Summary
    {
        private int totalTasks;
        private int numberCompleteTasks;
        private int numberIncompleteTasks;
        private int numberEasyTasksCompleted;
        private int numberMediumTasksCompleted;
        private int numberHardTasksCompleted;
        private DBConnector dbCon;

        Summary(int newTotalTasks, int newNumberCompleteTasks, int newNumberIncompleteTasks, 
                int newEasyTasksC, int newMediumTasksC, int newHardTasksC, DBConnector newDBCon)
        {
            totalTasks = newTotalTasks;
            numberCompleteTasks = newNumberCompleteTasks;
            numberIncompleteTasks = newNumberIncompleteTasks;
            numberEasyTasksCompleted = newEasyTasksC;
            numberMediumTasksCompleted = newMediumTasksC;
            numberHardTasksCompleted = newHardTasksC;
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

        public int getEasyTasks()
        {
            return numberEasyTasksCompleted;
        }

        public int getMediumTasks()
        {
            return numberMediumTasksCompleted;
        }

        public int getHardTasks()
        {
            return numberHardTasksCompleted;
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

        public void decrementIncompleteTasks()
        {
            numberIncompleteTasks--;
        }

        public void incrementEasyTasks()
        {
            numberEasyTasksCompleted++;
        }

        public void incrementMediumTasks()
        {
            numberMediumTasksCompleted++;
        }

        public void incrementHardTasks()
        {
            numberHardTasksCompleted++;
        }

        public void decrementEasyTasks()
        {
            numberEasyTasksCompleted--;
        }

        public void decrementMediumTasks()
        {
            numberMediumTasksCompleted--;
        }

        public void decrementHardTasks()
        {
            numberHardTasksCompleted--;
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
