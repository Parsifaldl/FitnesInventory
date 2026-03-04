using System;
using System.Windows;
using FitnesInventory.Models;

namespace FitnesInventory
{
    public partial class AddSimpleWindow : Window
    {
        private DatabaseService _dbService;
        private string _type;

        public AddSimpleWindow(DatabaseService dbService, string type)
        {
            InitializeComponent();
            _dbService = dbService;
            _type = type;

            switch (type)
            {
                case "EquipmentCategory":
                    Title = "Добавление категории оборудования";
                    lblField1.Content = "Название категории:";
                    lblField2.Visibility = Visibility.Visible;
                    txtField2.Visibility = Visibility.Visible;
                    lblField2.Content = "Описание:";
                    break;
                case "InventoryCategory":
                    Title = "Добавление категории инвентаря";
                    lblField1.Content = "Название категории:";
                    lblField2.Visibility = Visibility.Visible;
                    txtField2.Visibility = Visibility.Visible;
                    lblField2.Content = "Единица измерения:";
                    break;
                case "Location":
                    Title = "Добавление местоположения";
                    lblField1.Content = "Название местоположения:";
                    lblField2.Visibility = Visibility.Collapsed;
                    txtField2.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtField1.Text))
                {
                    MessageBox.Show("Заполните название");
                    return;
                }

                switch (_type)
                {
                    case "EquipmentCategory":
                        var equipCat = new EquipmentCategory
                        {
                            CategoryName = txtField1.Text,
                            Description = txtField2.Text
                        };
                        _dbService.AddEquipmentCategory(equipCat);
                        break;
                    case "InventoryCategory":
                        var invCat = new InventoryCategory
                        {
                            CategoryName = txtField1.Text,
                            UnitOfMeasure = txtField2.Text
                        };
                        _dbService.AddInventoryCategory(invCat);
                        break;
                    case "Location":
                        var location = new Location
                        {
                            LocationName = txtField1.Text
                        };
                        _dbService.AddLocation(location);
                        break;
                }

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