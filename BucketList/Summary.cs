using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketList
{
    class Summary
    {
        private int totalTasks;
        private int numberCompleteTasks;
        private int numberIncompleteTasks;

        Summary(int newTotalTasks, int newNumberCompleteTasks, int newNumberIncompleteTasks)
        {
            totalTasks = newTotalTasks;
            numberCompleteTasks = newNumberCompleteTasks;
            numberIncompleteTasks = newNumberIncompleteTasks;
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

        public void loadSummaryFromDB(SqlConnection con)
        {

        }
    }
}
