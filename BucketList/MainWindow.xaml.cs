using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public MainWindow()
        {
            InitializeComponent();

            BucketListTask task = new BucketListTask("Test Task", "Hard", "Test Description");
            bucketListTasks.Add(task);
            this.DataContext = bucketListTasks;
        }

        private void addItemButton_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addTaskWindow = new AddTaskWindow();

            if (addTaskWindow.ShowDialog() == true)
            {
                BucketListTask bucketListTask = new BucketListTask(addTaskWindow.taskName, addTaskWindow.difficultyTextBox.Text, addTaskWindow.taskDescription);
                bucketListTasks.Add(bucketListTask);
            }
        }

        private void removeItemButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void summaryButton_Click(object sender, RoutedEventArgs e)
        {

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
