using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace treker_sna_001
{
    /// <summary>
    /// Логика взаимодействия для addNoteWindow.xaml
    /// </summary>
    public partial class addNoteWindow : Window
    {
    
        public addNoteWindow()
        {
            InitializeComponent();
            InitializeComboBoxes();
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

        private void InitializeComboBoxes()
        {
            // Заполняем списки часов
            for (int i = 0; i <= 23; i++)
            {
                StartHourComboBox.Items.Add(i.ToString("D2")); // Форматируем для отображения с ведущим нулем
                EndHourComboBox.Items.Add(i.ToString("D2"));
            }

            // Заполняем списки минут
            for (int i = 0; i <= 59; i++)
            {
                StartMinuteComboBox.Items.Add(i.ToString("D2"));  // Форматируем для отображения с ведущим нулем
                EndMinuteComboBox.Items.Add(i.ToString("D2"));
            }

            //заполняем список количества пробуждений
            for (int i = 0; i <= 10; i++)
            {
                countUp.Items.Add(i.ToString());
            }
            // Устанавливаем значения по умолчанию
            StartHourComboBox.SelectedIndex = 0;
            StartMinuteComboBox.SelectedIndex = 0;
            EndHourComboBox.SelectedIndex = 0;
            EndMinuteComboBox.SelectedIndex = 0;
            countUp.SelectedIndex = 0;
        }

        public int startHour;
        public int startMinute;
        public int endHour;
        public int endMinute;
        public string res2;
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                // Получаем выбранные значения из списков
                startHour = int.Parse(StartHourComboBox.SelectedItem.ToString());
                startMinute = int.Parse(StartMinuteComboBox.SelectedItem.ToString());
                endHour = int.Parse(EndHourComboBox.SelectedItem.ToString());
                endMinute = int.Parse(EndMinuteComboBox.SelectedItem.ToString());

                // Вычисляем разницу во времени
                TimeSpan timeDifference = CalculateTimeDifference(startHour, startMinute, endHour, endMinute);

                // Отображаем результат
                ResultLabel.Content = $"Разница: {timeDifference.Hours} часов, {timeDifference.Minutes} минут";
                res2 = $"{timeDifference.Hours}:{timeDifference.Minutes}";
            }
            catch (Exception ex)
            {
                ResultLabel.Content = $"Ошибка: {ex.Message}";
            }

            if (startHour > 19 || startHour < 2)
            {
                MessageBox.Show("Нормальное время засыпания");
            }
            stress_Checked();
        }

        DateTime startTime;
        DateTime endTime;
        private TimeSpan CalculateTimeDifference(int startHour, int startMinute, int endHour, int endMinute)
        {
            startTime = new DateTime(1, 1, 1, startHour, startMinute, 0);
            endTime = new DateTime(1, 1, 1, endHour, endMinute, 0);

            // Обрабатываем случай, когда конечное время раньше начального (переход через полночь)
            if (endTime < startTime)
            {
                endTime = endTime.AddDays(1);  // Предполагаем, что конечное время на следующий день
            }

            return endTime - startTime;
        }
        //тип сна
        string TypeDream;
        private void rbP_Checked(object sender, RoutedEventArgs e)
        {
            TypeDream = "Поверхностный";
        }

        private void rbG_Checked(object sender, RoutedEventArgs e)
        {
            TypeDream = "Глубокий";
        }
        //ощущения
        string Feel;
        private void rbB_Checked(object sender, RoutedEventArgs e)
        {
            Feel = "Бодрость";
        }

        private void rbR_Checked(object sender, RoutedEventArgs e)
        {
            Feel = "Разбитость";
        }
        

        //температура в спальне
        string temperature;
        private void coldTemperature_Checked(object sender, RoutedEventArgs e)
        {
            temperature = "Холод";
        }

        private void hotTemperature_Checked(object sender, RoutedEventArgs e)
        {
            temperature = "Жара";
        }

        private void normTemperature_Checked(object sender, RoutedEventArgs e)
        {
            temperature = "Нормальная";
        }
        //Счетчик пробуждений
        int countup;
        //внешние факторы
        string phis;
        string stres;
        //ID пользователя
        int id;
        private void stress_Checked()
        {
            countup = countUp.SelectedIndex;
            if (stress.IsChecked == true)
            {
                stres = "ДА";
            }
            else
            {
                stres = "НЕТ";
            }
            if(phisicalAktivities.IsChecked == true)
            {
                phis = "ДА";
            }
            else
            {
                phis = "НЕТ";
            }
            id = GlobalData.user.IdUser;
            saveDB();
        }
        private void saveDB()
        {
            Journal journal = new Journal();
            journal.UserIdUser = id;
            journal.TypeDream = TypeDream;
            journal.Feelings = Feel;
            journal.WakeUpCount = countup;
            journal.TimeDown = startTime;
            journal.TimeWakeUp = endTime;
            journal.Stress = stres;
            journal.Phisical = phis;
            journal.Temperature = temperature;

            App.db.Journals.Add(journal);
            App.db.SaveChanges();
            Close();
        }
        
    }
}
