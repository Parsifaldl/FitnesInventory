using FitnesInventory.Models;
using System;
using System.Windows;
using System.Xml.Linq;

namespace FitnesInventory
{
    public partial class AddSupplierWindow : Window
    {
        private DatabaseService _dbService;

        public AddSupplierWindow(DatabaseService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Введите название поставщика");
                    return;
                }

                var supplier = new Supplier
                {
                    SupplierName = txtName.Text,
                    ContactPhone = txtPhone.Text,
                    ContactEmail = txtEmail.Text,
                    Address = txtAddress.Text
                };

                _dbService.AddSupplier(supplier);

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}