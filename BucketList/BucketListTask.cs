using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketList
{
    public class BucketListTask
    {
        public string name { get; set; }
        public string difficulty { get; set; }
        public string description { get; set; }
        public double cost { get; set; }
        public string location { get; set; }
        public string memoryPath { get; set; }
        public string dateCompleted { get; set; }
        public bool isComplete { get; set; }

        public BucketListTask(string newName, string newDifficulty, string newDescription, double newCost,
                              string newLocation, string newMemoryPath, string newDateCompleted, bool newIsComplete)
        {
            name = newName;
            difficulty = newDifficulty;
            description = newDescription;
            cost = newCost;
            location = newLocation;
            memoryPath = newMemoryPath;
            dateCompleted = newDateCompleted;
            isComplete = newIsComplete;
        }

        public void completeTask()
        {
            isComplete = true;
        }
    }
}
