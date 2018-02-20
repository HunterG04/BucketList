using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace BucketList
{
    /// <summary>
    /// Interaction logic for ViewTaskWindow.xaml
    /// </summary>
    public partial class ViewTaskWindow : Window
    {
        BucketListTask bucketListTask;

        public ViewTaskWindow(BucketListTask task)
        {
            InitializeComponent();

            bucketListTask = task;

            fillTaskInfo();
        }

        private void fillTaskInfo()
        {
            taskNameLabel.Content = bucketListTask.name;
            descriptionLabel.Content = "Description: " + bucketListTask.description;
            
            if (bucketListTask.isComplete)
            {
                dateCompletedLabel.Content = "Date Completed: " + bucketListTask.dateCompleted;
                costLabel.Content = "Cost: " + bucketListTask.cost;
                locationLabel.Content = "Location: " + bucketListTask.location;
            }
            else
            {
                dateCompletedLabel.Content = "Date Completed: Not Complete";
                costLabel.Content = "Cost: Not Complete";
                locationLabel.Content = "Location: Not Complete";
            }
        }

        private void viewMemoriesButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
