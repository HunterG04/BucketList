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
        private SqlConnection con = new SqlConnection();
        private string conString = "Data Source=localhost; Initial Catalog=BucketList; Integrated Security=True";

        public MainWindow()
        {
            InitializeComponent();

            con.ConnectionString = conString;

            this.DataContext = bucketListTasks;

            fillListFromDB();
        }

        private void fillListFromDB()
        {
            List<int> indexes = new List<int>();

            bucketListTasks.Clear();

            int i = 0;

            while (i < 2)
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand();

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
                cmd.CommandText = query;
                adapter.SelectCommand = cmd;

                cmd.Connection = con;

                DataSet ds = new DataSet();
                adapter.Fill(ds);

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

                i++;
            }
        }

        private void addItemButton_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addTaskWindow = new AddTaskWindow();

            if (addTaskWindow.ShowDialog() == true)
            {
                string query = "INSERT INTO TASKS (NAME, DIFFICULTY, DESCRIPTION) " +
                    "VALUES('" + addTaskWindow.taskName + "', '" + addTaskWindow.taskDifficulty + "', '" + addTaskWindow.taskDescription + "')";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

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

            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
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

                // TODO: Update item in database
                BucketListTask task = bucketListTasks[index];
                string query = "UPDATE TASKS " +
                               "SET COST = '" + task.cost.ToString() + "', LOCATION = '" + task.location + 
                               "', ISCOMPLETE = 1, DATECOMPLETED = '" + task.dateCompleted + "'" +
                               "WHERE NAME = '" + task.name + "' AND DESCRIPTION = '" + task.description + "' ";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
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
