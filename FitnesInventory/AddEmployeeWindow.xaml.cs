using FitnesInventory.Data;
using FitnesInventory.Models;
using System;
using System.Windows;

namespace FitnesInventory
{
    public partial class AddEmployeeWindow : Window
    {
        private FitnesInventoryDbContext _context;

        public AddEmployeeWindow(FitnesInventoryDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var employee = new Employee
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Position = txtPosition.Text
                };

                _context.Employee.Add(employee);
                _context.SaveChanges();

                MessageBox.Show("Сотрудник добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}