using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Starter.Examples.MultiThread;

namespace Starter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tc_Main.SelectedIndex = 0;
            var bc = new Examples.BossPresence.BossPresence();
            var imagePath = bc.GetBossImage();

            var img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(imagePath, UriKind.Relative);
            img.EndInit();
            BossImage.Source = img;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            tc_Main.SelectedIndex = 1;
            var carsEconomy = new Examples.CarsEconomy.CarsEconomy();
            FuelResult.Text = carsEconomy.Calculate() ? "Converted file saved to disk." : "Failed generating file.";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            tc_Main.SelectedIndex = 2;
            var lisp = new Examples.InterLisp.InterLisp();
            DemoResult.Text = lisp.Execute(Constants.LispEx1);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            tc_Main.SelectedIndex = 2;
            MultiThreadTest multiThreadTest = new MultiThreadTest();
            multiThreadTest.ThreadStateChanged += ThreadStateChanged;
            multiThreadTest.Execute();
            DemoResult.Text = "Executing async tasks..." + Environment.NewLine;
        }

        public delegate void UpdateTextCallback(string message);

        private void ThreadStateChanged(object sender, EventArgs e)
        {
            DemoResult.Dispatcher.Invoke(new UpdateTextCallback(this.AppendText), new object[] { (e as ThreadEventArgs).Message + Environment.NewLine });
        }

        private void AppendText(string value)
        {
            DemoResult.Text += value;
        }
    }

}
