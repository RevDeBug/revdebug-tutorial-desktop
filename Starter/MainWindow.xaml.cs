using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Starter.Examples.MultiThread;
using Starter.Examples.Loops;
using System.Windows.Controls;

namespace Starter
{
    public partial class MainWindow
    {
        public Brush RdbRedBrush;
        public Brush RdbGrayBrush;

        //Use arrows in control panel to move through recording.
        public MainWindow()
        {
            InitializeComponent();
            var converter = new BrushConverter();
            RdbRedBrush = (Brush)converter.ConvertFromString("#d22027");
            RdbGrayBrush = (Brush)converter.ConvertFromString("#8c8c8d");
        }

        private void Button_Home_Click(object sender, MouseButtonEventArgs e)
        {
            ResetButtonsStyle();
            tc_Main.SelectedIndex = 0;
        }

        private void Button_Hello_Click(object sender, RoutedEventArgs e)
        {
            tc_Main.SelectedIndex = 1;
            ResetButtonsStyle();
            HelloButton.Foreground = RdbRedBrush;
        }

        private void Button_Exception_Click(object sender, RoutedEventArgs e)
        {
            tc_Main.SelectedIndex = 2;
            ResetButtonsStyle();
            ExceptionButton.Foreground = RdbRedBrush;
        }

        private void Button_Loops_Click(object sender, RoutedEventArgs e)
        {
            tc_Main.SelectedIndex = 4;
            ResetButtonsStyle();
            LoopsButton.Foreground = RdbRedBrush;
        }

        private void Button_Threads_Click(object sender, RoutedEventArgs e)
        {
            tc_Main.SelectedIndex = 3;
            ResetButtonsStyle();
            ThreadsButton.Foreground = RdbRedBrush;
        }

        public delegate void UpdateTextCallback(string message);

        private void ThreadStateChanged(object sender, EventArgs e)
        {
            DemoResult.Dispatcher.Invoke(new UpdateTextCallback(this.AppendText), new object[] { (e as ThreadEventArgs).Message + Environment.NewLine });
        }

        private void ResetButtonsStyle()
        {
            HelloButton.Foreground = RdbGrayBrush;
            ExceptionButton.Foreground = RdbGrayBrush;
            LoopsButton.Foreground = RdbGrayBrush;
            ThreadsButton.Foreground = RdbGrayBrush;
        }

        private void AppendText(string value)
        {
            DemoResult.Text += value;
        }

        //Move back and forward using arrows in control panel
        //Read variable values thanks to overlays in code.
        private void ExecuteGreeter(object sender, RoutedEventArgs e)
        {
            (sender as System.Windows.Controls.Button).Visibility = Visibility.Collapsed;
            TerminateButton.Visibility = Visibility.Visible;

            HelloResult.Text = CoreGreeter.Program.Greet();
        }

        //Exceptions are visible in Exeptions/Search view.
        //You can open it from RevDeBug menu.
        //Double click on marker to jump to associated state.
        private void ExecuteException(object sender, RoutedEventArgs e)
        {
            (sender as System.Windows.Controls.Button).Visibility = Visibility.Collapsed;
            TerminateButton.Visibility = Visibility.Visible;

            var carsEconomy = new Examples.CarsEconomy.CarsEconomy();
            FuelResult.Text = carsEconomy.Calculate() ? "Converted file saved to disk." : "Failed generating file.";
        }

        //Open Threads view from RevDeBug menu to see, how consequent threads was executed.
        //You can click on timeline to fast travel in your recording.
        private void ExecuteThreads(object sender, RoutedEventArgs e)
        {
            (sender as System.Windows.Controls.Button).Visibility = Visibility.Collapsed;
            TerminateButton.Visibility = Visibility.Visible;

            MultiThreadTest multiThreadTest = new MultiThreadTest();
            multiThreadTest.ThreadStateChanged += ThreadStateChanged;
            multiThreadTest.Execute();
            DemoResult.Text = "Executing async tasks..." + Environment.NewLine;
        }

        //Click on the blue arrow in control panel (Step Over Forward Current Thread), until you reach the foreach loop.
        private void ExecuteLoops(object sender, RoutedEventArgs e)
        {
            (sender as System.Windows.Controls.Button).Visibility = Visibility.Collapsed;
            TerminateButton.Visibility = Visibility.Visible; 

            var loops = new Looper();
            var myCars = loops.CollectCars();

            LoopsResult.Text = string.Empty;
            foreach ( var car in myCars)
            {
                //Now, when you are inside the loop click the Scope Stack tab in control panel.
                //Here you can easly iterate throug the loop.
                //Move your mouse over the ForEach pane in control panel and click on previous/next iteration buttons to see consequent cars
                LoopsResult.Text += car + Environment.NewLine;
            }
        }

        private void Terminate(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
       
    }
}
