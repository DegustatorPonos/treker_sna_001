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
            if(login != string.Empty && password != string.Empty)
            {
                User user = App.db.Users.FirstOrDefault(us => us.userLogin == login && us.userPassword == password);
                if (user == null)
                {
                    // Выдаём ошибку
                    MessageBox.Show("Пользователь не найден!");
                }
                // Если сотрудник найден
                else
                {
                    //запоминаем пользователя
                    GlobalData.user = user;
                    // Открываем главное окно
                    MainWindow window = new MainWindow();
                    window.Show();
                    // Закрываем текущее окно с авторизацией
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Небходимо заполнить все поля!");
            }
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            if (txtUser.Text != string.Empty && txtPass.Password != string.Empty)
            {
                if (App.db.Users.FirstOrDefault(us => us.userLogin == txtUser.Text) != null)
                {
                    // Выдаём ошибку
                    MessageBox.Show("Пользователь c таким логином уже существует!");
                    return;
                }
                User user = new User()
                {
                    userLogin = txtUser.Text,
                    userPassword = txtPass.Password
                };
                App.db.Users.Add(user);
                App.db.SaveChanges();
                btnLogin_Click(sender, e);
            }
            else if(txtUser.Text == string.Empty || txtPass.Password == string.Empty)
            {
                MessageBox.Show("Необходимо заполнить все поля");
            }
        }
    }
}
