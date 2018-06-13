using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BucketList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<BucketListTask> bucketListTasks = new ObservableCollection<BucketListTask>();
        private DBConnector dbCon = new DBConnector("Data Source=localhost; Initial Catalog=BucketList; Integrated Security=True");

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = bucketListTasks;

            fillListFromDB();
        }

        private void fillListFromDB()
        {
            bucketListTasks.Clear();

            for(int i = 0; i < 2; i++)
            {
                string query;
                if (i == 0)
                {
                    query = "SELECT * FROM Tasks "
                                   + "WHERE isComplete = 0";
                }
                else
                {
                    query = "SELECT * FROM Tasks "
                                    + " WHERE isComplete = 1";
                }

                DataSet ds = dbCon.queryDB(query);

                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        bool complete = Convert.ToBoolean(row["isComplete"]);
                        double cost = 0;
                        string location = "N/A";
                        string memoryPath = null;
                        string dateCompleted = "N/A";

                        if (row["cost"] != System.DBNull.Value)
                        {
                            cost = Convert.ToDouble(row["cost"]);
                        }

                        if (row["location"] != System.DBNull.Value)
                        {
                            location = row["location"].ToString();
                        }

                        if (row["memoryPath"] != System.DBNull.Value)
                        {
                            memoryPath = row["memoryPath"].ToString();
                        }

                        if (row["dateCompleted"] != System.DBNull.Value)
                        {
                            dateCompleted = row["dateCompleted"].ToString();
                        }

                        BucketListTask task = new BucketListTask(row["name"].ToString(), row["difficulty"].ToString(), row["description"].ToString(),
                                                                 cost, location, memoryPath,
                                                                 dateCompleted, complete);
                        bucketListTasks.Add(task);
                    }
                }
            }
        }

        private void addItemButton_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addTaskWindow = new AddTaskWindow();

            if (addTaskWindow.ShowDialog() == true)
            {
                string query = "INSERT INTO TASKS (NAME, DIFFICULTY, DESCRIPTION) " +
                    "VALUES('" + addTaskWindow.taskName + "', '" + addTaskWindow.taskDifficulty + "', '" + addTaskWindow.taskDescription + "')";

                dbCon.executeCommand(query);

                fillListFromDB();
            }
        }

        private void removeItemButton_Click(object sender, RoutedEventArgs e)
        {
            int index = bucketListView.SelectedIndex;
            string tempName = bucketListTasks[index].name;
            string tempDescription = bucketListTasks[index].description;

            // Remove item from UI
            bucketListTasks.RemoveAt(index);

            // Remove item from database
            string query = "DELETE FROM TASKS " +
                           "WHERE NAME = '" + tempName + "' " + "AND DESCRIPTION = '" + tempDescription + "'";

            dbCon.executeCommand(query);
        }

        private void summaryButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Simple query of completed tasks and incomplete tasks
        }

        private void completeTask(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = bucketListView.Items.IndexOf(item);

            CompleteTaskWindow completeTaskWindow = new CompleteTaskWindow(bucketListTasks[index]);

            if (completeTaskWindow.ShowDialog() == true)
            {
                ListViewItem row = bucketListView.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;
                row.Background = Brushes.LightGreen;

                Button b = (Button)sender;
                b.IsEnabled = false;

                BucketListTask task = bucketListTasks[index];
                string query = "UPDATE TASKS " +
                               "SET COST = '" + task.cost.ToString() + "', LOCATION = '" + task.location + 
                               "', ISCOMPLETE = 1, DATECOMPLETED = '" + task.dateCompleted + "'" +
                               "WHERE NAME = '" + task.name + "' AND DESCRIPTION = '" + task.description + "' ";

                dbCon.executeCommand(query);

                // TODO: Move completed item to end of list signifying it's completion

            }
        }

        private void viewTask(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = bucketListView.Items.IndexOf(item);

            ViewTaskWindow viewTaskWindow = new ViewTaskWindow(bucketListTasks[index]);
            viewTaskWindow.Show();
        }
    }
}
