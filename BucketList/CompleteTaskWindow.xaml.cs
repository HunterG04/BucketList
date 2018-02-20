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
    /// Interaction logic for CompleteTaskWindow.xaml
    /// </summary>
    public partial class CompleteTaskWindow : Window
    {
        BucketListTask bucketListTask;

        public CompleteTaskWindow(BucketListTask task)
        {
            InitializeComponent();

            bucketListTask = task;
            taskNameLabel.Content = bucketListTask.name;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void completeButton_Click(object sender, RoutedEventArgs e)
        {
            bucketListTask.dateCompleted = dateCompletedTextBox.Text;

            double cost = 0;
            if (double.TryParse(costTextBox.Text, out cost))
            {
                bucketListTask.cost = cost;
            }

            bucketListTask.location = locationTextBox.Text;
            bucketListTask.isComplete = true;

            DialogResult = true;
            this.Close();
        }
    }
}
