using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
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

namespace treker_sna_001
{
    /// <summary>
    /// Логика взаимодействия для AlarmDialog.xaml
    /// </summary>
    public partial class AlarmDialog : Window
    {
        private string soundFilePath = "sample.wav";
        SoundPlayer player;
        public AlarmDialog()
        {
            InitializeComponent();
            mess.Text = $"{GlobalData.SharedData}, вставай, на работу пора!";
            player = new SoundPlayer(soundFilePath);
            player.PlayLooping();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            player.Stop();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
