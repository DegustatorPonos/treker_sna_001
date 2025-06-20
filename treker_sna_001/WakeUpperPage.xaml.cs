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
       
        public WakeUpperPage()
        {
            InitializeComponent();
            InitializeComboBoxes();
            LoadWakeUpper();
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
            napomainanie.SelectedIndex = 0;
        }

        public void LoadWakeUpper()
        {
            alarmListBox.ItemsSource = App.db.WakeUpper.ToList();
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

                // создание будильника и запись его в БД
                string nap = napomainanie.SelectedItem.ToString();
                WakeUpper wakeUpper = new WakeUpper()
                {
                    UserIdUser = GlobalData.user.IdUser,
                    dateTime = alarmTime,
                    Occasion = nap
                };

                App.db.WakeUpper.Add(wakeUpper);
                App.db.SaveChanges();

                LoadWakeUpper();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите час и минуту.");
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if(alarmListBox.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать элемент");
                return;
            }
            WakeUpper wakeUpper = alarmListBox.SelectedItem as WakeUpper;
            App.db.WakeUpper.Remove(wakeUpper);
            App.db.SaveChanges();
            LoadWakeUpper();
            MessageBox.Show("Запись удалена");
        }
    }
}
