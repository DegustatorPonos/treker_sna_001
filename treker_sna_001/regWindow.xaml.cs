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
                List<User> users;
                users = App.db.Users.ToList();
                foreach (User user in users)
                    {
                        if (user.userLogin == login)
                        {
                            checkLogin.Text = string.Empty;
                            if (user.userPassword == password)
                            {
                                GlobalData.SharedData = login;
                                GlobalData.user = user;
                                MainWindow mainWindow = new MainWindow();
                                mainWindow.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Неверный пароль!");
                            }
                        }
                    }
            }
            /*if (txtUser.Text == "t" && txtPass.Password == "t")
            {
                GlobalData.SharedData = login;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                
                MessageBox.Show(GlobalData.SharedData);
                this.Close();
            }*/
        }
    }
}
