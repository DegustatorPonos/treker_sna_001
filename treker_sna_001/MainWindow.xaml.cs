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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace treker_sna_001
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            loginTXT.Content = GlobalData.user.userLogin.ToString();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Проверка каждую секунду
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private void openjournal_Click(object sender, RoutedEventArgs e)
        {
            frameTrans();
            mainFrame.Navigate(new journalPage());
            infoTextBlock.Text = "МЕНЮ ЖУРНАЛА";
        }

        private void openreminder_Click(object sender, RoutedEventArgs e)
        {
            frameTrans();
            mainFrame.Navigate(new WakeUpperPage());
            infoTextBlock.Text = "МЕНЮ НАПОМИНАНИЙ";
        }

        private void openhabit_Click(object sender, RoutedEventArgs e)
        {
            frameTrans();
            mainFrame.Navigate(new HabitPage());
            infoTextBlock.Text = "СТРАНИЦА СТАТИСТИКИ";
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void frameTrans()
        {

            Uri uri = new Uri("openFrameStyleMainWin.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(uri);
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

            btnStackPanel.Orientation = Orientation.Horizontal;
            mainFrame.Visibility = Visibility.Visible;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            var list = App.db.WakeUpper.ToList();
            foreach (WakeUpper wakeUpper in list)
            {
                if (now >= wakeUpper.dateTime)
                {
                    showAlarmDialog(wakeUpper);
                }
            }
        }

        private void showAlarmDialog(WakeUpper wakeUpper)
        {
            AlarmDialog alarmDialog = new AlarmDialog(wakeUpper);
            alarmDialog.ShowDialog();
            App.db.WakeUpper.Remove(wakeUpper);
            App.db.SaveChanges();

        }

        private void menu_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
