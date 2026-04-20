using FitnesInventory.Data;
using FitnesInventory.Models;
using System;
using System.Windows;

namespace FitnesInventory
{
    public partial class AddSupplierWindow : Window
    {
        private FitnesInventoryDbContext _context;

        public AddSupplierWindow(FitnesInventoryDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var supplier = new Supplier
                {
                    SupplierName = txtName.Text,
                    ContactPhone = txtPhone.Text,
                    ContactEmail = txtEmail.Text,
                    Address = txtAddress.Text
                };

                _context.Supplier.Add(supplier);
                _context.SaveChanges();

                MessageBox.Show("Поставщик добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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