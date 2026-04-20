using FitnesInventory.Data;
using FitnesInventory.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FitnesInventory
{
    public partial class AddTransactionWindow : Window
    {
        private FitnesInventoryDbContext _context;

        public AddTransactionWindow(FitnesInventoryDbContext context)
        {
            InitializeComponent();
            _context = context;
            LoadInventoryItems();
            LoadEmployees();
        }

        private void LoadInventoryItems()
        {
            var items = _context.Inventory_Item.ToList();
            cmbItem.ItemsSource = items;
            cmbItem.DisplayMemberPath = "ItemName";
            cmbItem.SelectedValuePath = "ItemId";
        }

        private void LoadEmployees()
        {
            var employees = _context.Employee.ToList();
            cmbEmployee.ItemsSource = employees;
            cmbEmployee.DisplayMemberPath = "LastName";
            cmbEmployee.SelectedValuePath = "EmployeeId";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var transaction = new InventoryTransaction
                {
                    ItemId = (int)cmbItem.SelectedValue,
                    TransactionType = (cmbType.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    TransactionDate = DateTime.Now,
                    EmployeeId = (int)cmbEmployee.SelectedValue
                };

                _context.Inventory_Transaction.Add(transaction);
                _context.SaveChanges();

                MessageBox.Show("Транзакция добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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