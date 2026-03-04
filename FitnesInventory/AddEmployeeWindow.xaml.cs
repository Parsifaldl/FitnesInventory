using System;
using System.Windows;
using FitnesInventory.Models;

namespace FitnesInventory
{
    public partial class AddEmployeeWindow : Window
    {
        private DatabaseService _dbService;

        public AddEmployeeWindow(DatabaseService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFirstName.Text))
                {
                    MessageBox.Show("¬ведите им€");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    MessageBox.Show("¬ведите фамилию");
                    return;
                }

                var employee = new Employee
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Position = txtPosition.Text
                };

                _dbService.AddEmployee(employee);

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ќшибка: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}