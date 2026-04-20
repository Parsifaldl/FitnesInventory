using FitnesInventory.Data;
using FitnesInventory.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FitnesInventory
{
    public partial class LoginWindow : Window
    {
        private FitnesInventoryDbContext _context;
        public User CurrentUser { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
            _context = new FitnesInventoryDbContext();

           // UsernameBox.Text = "admin"; //логин
           // PasswordBox.Password = "admin123"; //пароль

            PasswordBox.KeyDown += (s, e) => { if (e.Key == Key.Enter) Login(); };
            UsernameBox.KeyDown += (s, e) => { if (e.Key == Key.Enter) Login(); };
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void Login()
        {
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowError("Введите логин и пароль");
                return;
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null || user.PasswordHash != password)
            {
                ShowError("Неверный логин или пароль");
                return;
            }

            CurrentUser = user;
            user.LastLogin = DateTime.Now;
            _context.SaveChanges();

            var mainWindow = new MainWindow(user);
            mainWindow.Show();

            this.Close();
        }

        private void ShowError(string message)
        {
            ErrorText.Text = message;
            ErrorText.Visibility = Visibility.Visible;
        }
    }
}