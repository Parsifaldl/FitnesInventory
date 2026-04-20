using FitnesInventory.Data;
using FitnesInventory.Models;
using System;
using System.Windows;

namespace FitnesInventory
{
    public partial class AddSimpleWindow : Window
    {
        private FitnesInventoryDbContext _context;
        private string _type;

        public AddSimpleWindow(FitnesInventoryDbContext context, string type)
        {
            InitializeComponent();
            _context = context;
            _type = type;

            switch (type)
            {
                case "EquipmentCategory":
                    Title = "Добавление категории оборудования";
                    lblField2.Visibility = Visibility.Visible;
                    txtField2.Visibility = Visibility.Visible;
                    lblField2.Content = "Описание:";
                    break;
                case "Location":
                    Title = "Добавление локации";
                    lblField2.Visibility = Visibility.Collapsed;
                    txtField2.Visibility = Visibility.Collapsed;
                    break;
                case "InventoryCategory":
                    Title = "Добавление категории инвентаря";
                    lblField2.Visibility = Visibility.Visible;
                    txtField2.Visibility = Visibility.Visible;
                    lblField2.Content = "Ед. измерения:";
                    break;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (_type)
                {
                    case "EquipmentCategory":
                        var equipCategory = new EquipmentCategory
                        {
                            CategoryName = txtField1.Text,
                            Description = txtField2.Text
                        };
                        _context.Equipment_Category.Add(equipCategory);
                        break;
                    case "Location":
                        var location = new Location
                        {
                            LocationName = txtField1.Text
                        };
                        _context.Location.Add(location);
                        break;
                    case "InventoryCategory":
                        var invCategory = new InventoryCategory
                        {
                            CategoryName = txtField1.Text,
                            UnitOfMeasure = txtField2.Text
                        };
                        _context.Inventory_Category.Add(invCategory);
                        break;
                }

                _context.SaveChanges();
                MessageBox.Show("Добавлено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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