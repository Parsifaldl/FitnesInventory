using System;
using System.Windows;
using FitnesInventory.Models;

namespace FitnesInventory
{
    public partial class AddInventoryItemWindow : Window
    {
        private DatabaseService _dbService;

        public AddInventoryItemWindow(DatabaseService dbService)
        {
            InitializeComponent();
            _dbService = dbService;

            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            cmbCategory.ItemsSource = _dbService.GetInventoryCategories();
            cmbSupplier.ItemsSource = _dbService.GetSuppliers();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Введите название");
                    return;
                }

                if (cmbCategory.SelectedItem == null)
                {
                    MessageBox.Show("Выберите категорию");
                    return;
                }

                var item = new InventoryItem
                {
                    ItemName = txtName.Text,
                    CategoryId = ((InventoryCategory)cmbCategory.SelectedItem).CategoryId
                };

                if (cmbSupplier.SelectedItem != null)
                    item.SupplierId = ((Supplier)cmbSupplier.SelectedItem).SupplierId;

                if (int.TryParse(txtQuantity.Text, out int quantity))
                    item.CurrentQuantity = quantity;

                if (int.TryParse(txtMinStock.Text, out int minStock))
                    item.MinStockLevel = minStock;

                if (int.TryParse(txtMaxStock.Text, out int maxStock))
                    item.MaxStockLevel = maxStock;

                _dbService.AddInventoryItem(item);

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