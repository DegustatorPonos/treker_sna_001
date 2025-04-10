using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Path = System.IO.Path;

namespace treker_sna_001
{
    /// <summary>
    /// Логика взаимодействия для AlarmDialog.xaml
    /// </summary>
    public partial class AlarmDialog : Window
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        public AlarmDialog()
        {
            InitializeComponent();
            string resourcePath = "C:\\Users\\хорек2\\source\\repos\\treker_sna_001\\treker_sna_001\\music1";
            Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
            if (resourceStream != null)
            {
                // Создать временный файл
                string tempFilePath = Path.GetTempFileName() + ".mp3";

                // Записать поток в файл
                using (var fileStream = File.Create(tempFilePath))
                {
                    resourceStream.CopyTo(fileStream);
                }

                // Воспроизвести файл
                mediaPlayer.Open(new Uri(tempFilePath));
                mediaPlayer.Play();

                // Удалить временный файл после воспроизведения (опционально)
                mediaPlayer.MediaEnded += (s, args) =>
                {
                    try
                    {
                        File.Delete(tempFilePath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting temporary file: {ex.Message}");
                    }
                };

            }
            else
            {
                MessageBox.Show("Resource not found: " + resourcePath);
            }
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
