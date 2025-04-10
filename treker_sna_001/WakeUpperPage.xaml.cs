using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Media;
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
    /// Логика взаимодействия для WakeUpperPage.xaml
    /// </summary>
    public partial class WakeUpperPage : Page
    {
        List<string> napominanies = new List<string>()
        {
            "пробуждения",
            "засыпания",
            "другое"
        };

        public int Hour;
        public int Minute;
        private DateTime alarmTime;
        private DispatcherTimer timer;
        private string soundPath = "music1.mp3"; //путь к звуковому файлу

        public WakeUpperPage()
        {
            InitializeComponent();
            InitializeComboBoxes();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void InitializeComboBoxes()
        {
            // Заполняем списки часов
            for (int i = 0; i <= 23; i++)
            {
                HourComboBox.Items.Add(i.ToString("D2")); // Форматируем для отображения с ведущим нулем
            }

            // Заполняем списки минут
            for (int i = 0; i <= 59; i++)
            {
                MinuteComboBox.Items.Add(i.ToString("D2"));  // Форматируем для отображения с ведущим нулем
            }

            napomainanie.ItemsSource = napominanies;

            // Устанавливаем значения по умолчанию
            HourComboBox.SelectedIndex = 0;
            MinuteComboBox.SelectedIndex = 0;
        }

        private void Set_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранные значения из списков
            Hour = int.Parse(HourComboBox.SelectedItem.ToString());
            Minute = int.Parse(MinuteComboBox.SelectedItem.ToString());

            DateTime now = DateTime.Now;
            int hourNow = now.Hour;
            int minuteNow = now.Minute;

            TimeSpan timeDifference = CalculateTimeDifference(hourNow, minuteNow, Hour, Minute);
            MessageBox.Show($"Будильник сработает через {timeDifference.Hours.ToString()} часов {timeDifference.Minutes.ToString()} минут");
            alarm();
            AlarmDialog alarmDialog = new AlarmDialog();
            alarmDialog.ShowDialog();
        }

        private TimeSpan CalculateTimeDifference(int startHour, int startMinute, int endHour, int endMinute)
        {
            DateTime startTime = new DateTime(1, 1, 1, startHour, startMinute, 0);
            DateTime endTime = new DateTime(1, 1, 1, endHour, endMinute, 0);

            // Обрабатываем случай, когда конечное время раньше начального (переход через полночь)
            if (endTime < startTime)
            {
                endTime = endTime.AddDays(1);  // Предполагаем, что конечное время на следующий день
            }

            return endTime - startTime;
        }
        private void alarm()
        {
            string HHmm = $"{Hour}:{Minute}";
            if (DateTime.TryParseExact(HHmm, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime parsedTime))
            {
                DateTime today = DateTime.Today;
                alarmTime = new DateTime(today.Year, today.Month, today.Day, parsedTime.Hour, parsedTime.Minute, 0);
                if (alarmTime <= DateTime.Now)
                {
                    alarmTime = alarmTime.AddDays(1);
                }
                MessageBox.Show($"{alarmTime.ToString("HH.mm")} {alarmTime.ToShortDateString()}");
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now >= alarmTime)
            {
                timer.Stop();
                showAlarmDialog();
            }
        }
        private void showAlarmDialog()
        {
            AlarmDialog alarmDialog = new AlarmDialog();
            alarmDialog.ShowDialog();

            /*try
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri (soundPath));
                alarmDialog.Closed += (s, args) => { player.Stop(); };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
            }*/
        }



    }
}
