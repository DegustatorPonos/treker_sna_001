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

        private List<DateTime> alarms = new List<DateTime>();
        private DispatcherTimer timer;

        public WakeUpperPage()
        {
            InitializeComponent();
            InitializeComboBoxes();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Проверка каждую секунду
            timer.Tick += Timer_Tick;
            timer.Start();
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
            if (HourComboBox.SelectedItem != null && MinuteComboBox.SelectedItem != null)
            {
                int hour = int.Parse(HourComboBox.SelectedItem.ToString());
                int minute = int.Parse(MinuteComboBox.SelectedItem.ToString());

                DateTime alarmTime = DateTime.Now.Date.AddHours(hour).AddMinutes(minute);
                if (alarmTime <= DateTime.Now)
                {
                    alarmTime = alarmTime.AddDays(1); // Переносим на завтра, если время уже прошло сегодня
                }

                alarms.Add(alarmTime);
                alarmListBox.Items.Add(alarmTime.ToString("HH:mm dd.MM.yyyy"));
                //UpdateStatus("Будильник установлен на " + alarmTime.ToString("HH:mm dd.MM.yyyy"));
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите час и минуту.");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            for (int i = alarms.Count - 1; i >= 0; i--) // Итерируем с конца, чтобы безопасно удалять элементы
            {
                if (now >= alarms[i])
                {
                    showAlarmDialog();
                    alarms.RemoveAt(i);
                    alarmListBox.Items.RemoveAt(i);
                    //UpdateStatus("Будильник сработал и удален.");
                }
            }
        }
        private void showAlarmDialog()
        {
            AlarmDialog alarmDialog = new AlarmDialog();
            alarmDialog.ShowDialog();
        }
    }
}
