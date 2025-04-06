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
                WriterBDtxt();
            }
            catch (Exception ex)
            {
                ResultLabel.Content = $"Ошибка: {ex.Message}";
            }

            if (startHour > 19 || startHour < 2)
            {
                MessageBox.Show("Нормальное время засыпания");
            }
            else
            {
                MessageBox.Show("[eqyz");
            }
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

        private void WriterBDtxt()
        {
            using (StreamWriter sw = new StreamWriter("bdtest.txt", true))
            {
                sw.WriteLine(res2);
            }
        }
    }
}
