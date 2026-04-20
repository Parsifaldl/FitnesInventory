using FitnesInventory.Data;
using FitnesInventory.Models;
using System;
using System.Linq;
using System.Windows;

namespace FitnesInventory
{
    public partial class UserManagementWindow : Window
    {
        private FitnesInventoryDbContext _context;
        private User _currentAdmin;

        public UserManagementWindow(FitnesInventoryDbContext context, User adminUser)
        {
            InitializeComponent();
            _context = context;
            _currentAdmin = adminUser;
            LoadUsers();
        }

        private void LoadUsers()
        {
            var users = _context.Users.ToList();
            UsersGrid.ItemsSource = users;
            StatusText.Text = $"Загружено пользователей: {users.Count}";
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditUserWindow(_context, null);
            dialog.Owner = this;
            if (dialog.ShowDialog() == true)
            {
                LoadUsers();
            }
        }

        private void EditUserBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected = UsersGrid.SelectedItem as User;
            if (selected == null)
            {
                MessageBox.Show("Выберите пользователя для редактирования", "Предупреждение",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selected.Role == "Admin" && selected.UserId != _currentAdmin.UserId)
            {
                MessageBox.Show("Вы не можете редактировать другого администратора", "Доступ запрещён",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dialog = new AddEditUserWindow(_context, selected);
            dialog.Owner = this;
            if (dialog.ShowDialog() == true)
            {
                LoadUsers();
            }
        }

        private void DeleteUserBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected = UsersGrid.SelectedItem as User;
            if (selected == null)
            {
                MessageBox.Show("Выберите пользователя для удаления", "Предупреждение",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selected.Role == "Admin")
            {
                MessageBox.Show("Нельзя удалить администратора", "Доступ запрещён",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selected.Username == _currentAdmin.Username)
            {
                MessageBox.Show("Нельзя удалить самого себя", "Доступ запрещён",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Удалить пользователя '{selected.Username}'?", "Подтверждение",
                                         MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _context.Users.Remove(selected);
                _context.SaveChanges();
                LoadUsers();
            }
        }
    }
}