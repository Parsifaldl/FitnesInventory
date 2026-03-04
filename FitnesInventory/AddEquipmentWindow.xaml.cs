using FitnesInventory.Data;
using FitnesInventory.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FitnesInventory
{
    public partial class AddEquipmentWindow : Window
    {
        private DatabaseService _dbService;
        private Equipment _equipment;

        public AddEquipmentWindow(DatabaseService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            _equipment = new Equipment();

            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            cmbCategory.ItemsSource = _dbService.GetEquipmentCategories();
            cmbLocation.ItemsSource = _dbService.GetLocations();
            cmbSupplier.ItemsSource = _dbService.GetSuppliers();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Введите название оборудования");
                    return;
                }

                if (cmbCategory.SelectedItem == null)
                {
                    MessageBox.Show("Выберите категорию");
                    return;
                }

                _equipment.EquipmentName = txtName.Text;
                _equipment.SerialNumber = txtSerial.Text;
                _equipment.CategoryId = ((EquipmentCategory)cmbCategory.SelectedItem).CategoryId;

                if (cmbLocation.SelectedItem != null)
                    _equipment.LocationId = ((Location)cmbLocation.SelectedItem).LocationId;

                if (cmbSupplier.SelectedItem != null)
                    _equipment.SupplierId = ((Supplier)cmbSupplier.SelectedItem).SupplierId;

                _equipment.PurchaseDate = dpDate.SelectedDate;

                if (decimal.TryParse(txtPrice.Text, out decimal price))
                    _equipment.PurchasePrice = price;

                if (cmbStatus.SelectedItem is ComboBoxItem item)
                    _equipment.Status = item.Content.ToString();

                _dbService.AddEquipment(_equipment);

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