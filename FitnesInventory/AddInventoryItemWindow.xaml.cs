using FitnesInventory.Data;
using FitnesInventory.Models;
using System;
using System.Linq;
using System.Windows;

namespace FitnesInventory
{
    public partial class AddInventoryItemWindow : Window
    {
        private FitnesInventoryDbContext _context;

        public AddInventoryItemWindow(FitnesInventoryDbContext context)
        {
            InitializeComponent();
            _context = context;
            LoadCategories();
            LoadSuppliers();
        }

        private void LoadCategories()
        {
            var categories = _context.Inventory_Category.ToList();
            cmbCategory.ItemsSource = categories;
            cmbCategory.DisplayMemberPath = "CategoryName";
            cmbCategory.SelectedValuePath = "CategoryId";
        }

        private void LoadSuppliers()
        {
            var suppliers = _context.Supplier.ToList();
            cmbSupplier.ItemsSource = suppliers;
            cmbSupplier.DisplayMemberPath = "SupplierName";
            cmbSupplier.SelectedValuePath = "SupplierId";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = new InventoryItem
                {
                    ItemName = txtName.Text,
                    CategoryId = (int)cmbCategory.SelectedValue,
                    SupplierId = (int)cmbSupplier.SelectedValue,
                    CurrentQuantity = int.Parse(txtQuantity.Text),
                    MinStockLevel = int.Parse(txtMinStock.Text),
                    MaxStockLevel = int.Parse(txtMaxStock.Text)
                };

                _context.Inventory_Item.Add(item);
                _context.SaveChanges();

                MessageBox.Show("Инвентарь добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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