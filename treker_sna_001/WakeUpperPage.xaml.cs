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
                StartHourComboBox.Items.Add(i.ToString("D2")); // Форматируем для отображения с ведущим нулем
            }

            // Заполняем списки минут
            for (int i = 0; i <= 59; i++)
            {
                StartMinuteComboBox.Items.Add(i.ToString("D2"));  // Форматируем для отображения с ведущим нулем
            }

            // Устанавливаем значения по умолчанию
            StartHourComboBox.SelectedIndex = 0;
            StartMinuteComboBox.SelectedIndex = 0;
        }
    }
}
