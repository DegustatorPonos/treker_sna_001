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
    /// Логика взаимодействия для journalPage.xaml
    /// </summary>
    public partial class journalPage : Page
    {
        public journalPage()
        {
            InitializeComponent();
        }

        private void addnote_Click(object sender, RoutedEventArgs e)
        {
            addNoteWindow add = new addNoteWindow();
            add.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        
    }
}
