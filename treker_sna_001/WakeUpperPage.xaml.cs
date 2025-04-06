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

        public WakeUpperPage()
        {
            InitializeComponent();
            InitializeComboBoxes();
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


        }
    }
}
