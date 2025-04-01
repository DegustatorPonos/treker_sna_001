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

namespace treker_sna_001
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //mainFrame.Navigate(new Test1());
        }

        private void openjournal_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("openFrameStyleMainWin.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(uri);
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

            btnStackPanel.Orientation = Orientation.Horizontal;
            mainFrame.Visibility = Visibility.Visible;
                
            mainFrame.Navigate(new journalPage());
        }

        private void openreminder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Открыть меню напоминаний");
        }

        private void openhabit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Открыть меню привычек");
        }
    }
}
