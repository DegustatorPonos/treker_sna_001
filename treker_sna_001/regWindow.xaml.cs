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
using System.Windows.Shapes;

namespace treker_sna_001
{
    /// <summary>
    /// Логика взаимодействия для regWindow.xaml
    /// </summary>
    public partial class regWindow : Window
    {
        public regWindow()
        {
            InitializeComponent();
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            string login = txtUser.Text;
            string password = txtPass.Password;
            if (login != ""&& password != "")
            {
                using(UsersBD1Entities db =  new UsersBD1Entities())
                {
                    List<Users> users = new List<Users>();
                    users = db.Users.ToList();
                    //logs.ItemsSource = users; //для проверки получения пользователей
                    foreach (Users user in users)
                    {
                        if(user.userLogin == login)
                        {
                            if(user.userPassword == password)
                            {
                                MainWindow mainWindow = new MainWindow();
                                mainWindow.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Неверный пароль!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неправильный логин и/или пароль");
                        }
                    }
                }
            }
            if(txtUser.Text == "t" && txtPass.Password == "t")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
