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
        public int userID = GlobalData.user.IdUser;
        public journalPage()
        {
            InitializeComponent();
            LoadNotes();
        }

        private void addnote_Click(object sender, RoutedEventArgs e)
        {
            addNoteWindow add = new addNoteWindow();
            if(add.ShowDialog() == true)
            {
                LoadNotes();
            }
        }

        private void LoadNotes()
        {
            JournalDataGrid.ItemsSource = App.db.Journals.Where(j => j.UserIdUser == userID).ToList();
        }

        private void delnote_Click(object sender, RoutedEventArgs e)
        {
            if (JournalDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись!");
                return;
            }
            if(MessageBox.Show("удалить?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Journal journal = JournalDataGrid.SelectedItem as Journal;
                App.db.Journals.Remove(journal);
                App.db.SaveChanges();
                MessageBox.Show("Данные удалены");
                LoadNotes();
            }
            
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            Journal journal = new Journal();
            if(MessageBox.Show("Очистить?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                List<Journal> list = App.db.Journals.Where(j => j.UserIdUser == userID).ToList();
                foreach(Journal journal1 in list)
                {
                    App.db.Journals.Remove(journal1);
                }
                App.db.SaveChanges();
                LoadNotes();
            }
        }
    }
}
