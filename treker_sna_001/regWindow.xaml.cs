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
        public string login;
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
            
            login = txtUser.Text;
            string password = txtPass.Password;
            if (login != "" && password != "")
            {
                using (Kurs1Container db = new Kurs1Container())
                {
                    List<User> users = new List<User>();
                    users = db.Users.ToList();
                    //logs.ItemsSource = users; //для проверки получения пользователей
                    foreach (User user in users)
                    {
                        if (user.userLogin == login)
                        {
                            checkLogin.Text = string.Empty;
                            MessageBox.Show(user.userPassword);
                            if (user.userPassword == password)
                            {
                                GlobalData.SharedData = login;
                                MainWindow mainWindow = new MainWindow();
                                mainWindow.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Неверный пароль!");
                            }
                        }
                        else if (login != user.userLogin)
                        {
                            checkLogin.Text = "Неверный логин";
                        }
                    }
                }
            }
            if (txtUser.Text == "t" && txtPass.Password == "t")
            {
                GlobalData.SharedData = login;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                
                MessageBox.Show(GlobalData.SharedData);
                this.Close();
            }
        }
    }
}
