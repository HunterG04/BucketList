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
    /// Interaction logic for TaskSummaryWindow.xaml
    /// </summary>
    public partial class TaskSummaryWindow : Window
    {
        private Summary summary;

        public TaskSummaryWindow(Summary newSummary)
        {
            InitializeComponent();

            summary = newSummary;

            adjustLabelColors();
            setSummaryValues();
        }

        private void adjustLabelColors()
        {
            EasyLabel.Foreground = Brushes.Green;
            MediumLabel.Foreground = Brushes.Yellow;
            HardLabel.Foreground = Brushes.Red;
        }

        private void setSummaryValues()
        {
            // Set the summary labels for tasks completed and incomplete
            TotalTasksLabel.Content = TotalTasksLabel.Content + summary.getTotalTasks().ToString();
            CompleteTasksLabel.Content = CompleteTasksLabel.Content + summary.getTasksCompleted().ToString();
            IncompleteTasksLabel.Content = IncompleteTasksLabel.Content + summary.getIncompleteTasks().ToString();

            // Set the labels for easy, medium, and hard challenges completed
            EasyLabel.Content = summary.getEasyTasks().ToString();
            MediumLabel.Content = summary.getMediumTasks().ToString();
            HardLabel.Content = summary.getHardTasks().ToString();
        }
    }
}
