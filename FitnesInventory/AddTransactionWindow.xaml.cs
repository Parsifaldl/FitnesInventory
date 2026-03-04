using System;
using System.Windows;
using System.Windows.Controls;
using FitnesInventory.Models;

namespace FitnesInventory
{
    public partial class AddTransactionWindow : Window
    {
        private DatabaseService _dbService;

        public AddTransactionWindow(DatabaseService dbService)
        {
            InitializeComponent();
            _dbService = dbService;

            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            cmbItem.ItemsSource = _dbService.GetAllInventoryItems();
            cmbEmployee.ItemsSource = _dbService.GetEmployees();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbItem.SelectedItem == null)
                {
                    MessageBox.Show("Выберите товар");
                    return;
                }

                var transaction = new InventoryTransaction
                {
                    ItemId = ((InventoryItem)cmbItem.SelectedItem).ItemId,
                    TransactionType = ((ComboBoxItem)cmbType.SelectedItem).Content.ToString(),
                    TransactionDate = DateTime.Now
                };

                if (cmbEmployee.SelectedItem != null)
                    transaction.EmployeeId = ((Employee)cmbEmployee.SelectedItem).EmployeeId;

                _dbService.AddTransaction(transaction);

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