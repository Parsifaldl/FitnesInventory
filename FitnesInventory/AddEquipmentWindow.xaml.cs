using FitnesInventory.Data;
using FitnesInventory.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FitnesInventory
{
    public partial class AddEquipmentWindow : Window
    {
        private FitnesInventoryDbContext _context;

        public AddEquipmentWindow(FitnesInventoryDbContext context)
        {
            InitializeComponent();
            _context = context;
            LoadCategories();
            LoadLocations();
            LoadSuppliers();
        }

        private void LoadCategories()
        {
            var categories = _context.Equipment_Category.ToList();
            cmbCategory.ItemsSource = categories;
            cmbCategory.DisplayMemberPath = "CategoryName";
            cmbCategory.SelectedValuePath = "CategoryId";
        }

        private void LoadLocations()
        {
            var locations = _context.Location.ToList();
            cmbLocation.ItemsSource = locations;
            cmbLocation.DisplayMemberPath = "LocationName";
            cmbLocation.SelectedValuePath = "LocationId";
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
                var equipment = new Equipment
                {
                    EquipmentName = txtName.Text,
                    SerialNumber = txtSerial.Text,
                    CategoryId = (int)cmbCategory.SelectedValue,
                    LocationId = (int)cmbLocation.SelectedValue,
                    SupplierId = (int)cmbSupplier.SelectedValue,
                    PurchaseDate = dpDate.SelectedDate,
                    PurchasePrice = decimal.Parse(txtPrice.Text),
                    Status = (cmbStatus.SelectedItem as ComboBoxItem)?.Content.ToString()
                };

                _context.Equipment.Add(equipment);
                _context.SaveChanges();

                MessageBox.Show("Оборудование добавлено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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