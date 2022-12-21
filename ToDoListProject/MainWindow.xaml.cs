using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoListProject.Constants;
using ToDoListProject.Models;
using ToDoListProject.DataAccessFolder;

namespace ToDoListProject
{
  
    public partial class MainWindow : Window
    {
        #region Field

        private DataAccess _data;
        public int ListIndex = 1;

        #endregion    

        public MainWindow()
        {
            InitializeComponent();
           _data = new DataAccess();
            Load_ListOfTasks(ListIndex);
        }

        #region Methods

        private async void Load_ListOfTasks(int num)     // Refresh And Update The List By List Index
        {
            try
            {
                ListIndex = num;
                var list = await _data.GetAllTasks();

                switch (num)
                {
                    case 1:
                        list = await _data.GetAllTasksIsDone(false);
                        NameListIndector.Text = "To Do";
                        break;
                    case 2:
                        list = await _data.GetAllTasksIsDone(true);
                        NameListIndector.Text = "Done";
                        break;
                    case 3:
                        list = await _data.GetAllTasks();
                        NameListIndector.Text = "Tasks";
                        break;
                }
                ListOfTasks.ItemsSource = list;
            }
            catch (Exception) { }
        }

        private bool TaskNameIsEmpty()
        {
            var result = NameTask.Text != String.Empty ? false : true;
            return result;

        }

        #endregion

        #region Buttons Events

        private void Close_Win_Btn_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown(); // Close Application Btn click

        private void Min_Win_Btn_Click(object sender, RoutedEventArgs e) => Application.Current.MainWindow.WindowState = WindowState.Minimized;   // Minimize Application Btn click
      
        private void Open_Create_Canvas_Btn_Click(object sender, RoutedEventArgs e) => Create_Task_Canvas.Visibility = Visibility.Visible; // Open Create Task Menu

        private void Close_Create_Canvas_Btn_Click(object sender, RoutedEventArgs e) => Create_Task_Canvas.Visibility = Visibility.Collapsed; // Close Create Task Menu

        private async void Todo_Btn_Menu_Click(object sender, RoutedEventArgs e) => Load_ListOfTasks(1);   // Open The To Do List

        private async void Done_Btn_Menu_Click(object sender, RoutedEventArgs e) => Load_ListOfTasks(2);   // Open The Done List

        private async void Tasks_Btn_Menu_Click(object sender, RoutedEventArgs e) => Load_ListOfTasks(3);   // Open All Tasks List

        private void TopBar_Move_Click(object sender, MouseButtonEventArgs e)    //Make The Top bar Moveable
        { if (e.LeftButton == MouseButtonState.Pressed) this.DragMove(); }   
            
        private void Close_SideBar_Btn_Click(object sender, RoutedEventArgs e)    //Close Side Bar 
        {
            Title_Canvas.Visibility = Visibility.Collapsed;      // Collapse the title section
            SideBar_Canvas.Visibility = Visibility.Collapsed;            // Collapse the SideBar List section
            ListOfTasks.SetValue(Grid.ColumnProperty, 0);
            ListOfTasks.SetValue(Grid.ColumnSpanProperty, 3);
        }

        private void Expand_SideBar_Btn_Click(object sender, RoutedEventArgs e)  // Open Side Bar
        {
            Title_Canvas.Visibility = Visibility.Visible;           // Make Visible the title section
            SideBar_Canvas.Visibility = Visibility.Visible;                 // Make Visible the SideBar List section
            ListOfTasks.SetValue(Grid.ColumnProperty, 1);
            ListOfTasks.SetValue(Grid.ColumnSpanProperty, 2);
        }

        private async void Creat_Task_Btn_Click(object sender, RoutedEventArgs e)   //Create Task Method From Text Blocks Info
        {

            if (TaskNameIsEmpty() == false)
            {
                try { await _data.CreateTask(new TaskModel(NameTask.Text) { Description = DescriptionTask.Text }); }

                catch (Exception) { MessageBox.Show("Fail To Create New Task", Constants.ErrorCodes.FAIL_NAME); }

                Load_ListOfTasks(ListIndex);
                NameTask.Text = DescriptionTask.Text = string.Empty;
            }
            else if (TaskNameIsEmpty() == true) 
                MessageBox.Show(Constants.ErrorCodes.FAILED_TASK_NAME_EMPTY,Constants.ErrorCodes.FAIL_NAME);
            
        }

        private async void Make_Done_Btn_Click(object sender, RoutedEventArgs e)    // Make Task Done By Selection Of ListOfTasks
        {
            try
            {
                var task = ListOfTasks.SelectedItem as TaskModel;
                await _data.MadeTaskDone(task);
                Load_ListOfTasks(ListIndex);
            }
            catch (Exception )
            { MessageBox.Show(Constants.ErrorCodes.FAILED_TO_SELECT_TASK , Constants.ErrorCodes.FAIL_NAME); }

        }

        private async void Delete_Task_Btn_Click(object sender, RoutedEventArgs e) // Delete Task By Selection Of ListOfTasks
        {
            try
            {
                var task = ListOfTasks.SelectedItem as TaskModel;
                await _data.DeleteTask(task);
                Load_ListOfTasks(ListIndex);
                MessageBox.Show("Task Been Deleted", "Alert");
            }
            catch (Exception) 
            { MessageBox.Show(Constants.ErrorCodes.FAILED_TO_SELECT_TASK, Constants.ErrorCodes.FAIL_NAME); }

        }

            #endregion

    }
}
