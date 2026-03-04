using FitnesInventory.Data;
using FitnesInventory.Models;
using System;
using System.Windows;

namespace FitnesInventory
{
    public partial class MainWindow : Window
    {
        private DatabaseService _dbService;

        public MainWindow()
        {
            InitializeComponent();
            _dbService = new DatabaseService();
            LoadAllData();
        }

        private void LoadAllData()
        {
            try
            {
                EquipmentGrid.ItemsSource = _dbService.GetAllEquipment();
                InventoryGrid.ItemsSource = _dbService.GetAllInventoryItems();
                TransactionsGrid.ItemsSource = _dbService.GetAllTransactions();
                EquipmentCategoriesGrid.ItemsSource = _dbService.GetEquipmentCategories();
                LocationsGrid.ItemsSource = _dbService.GetLocations();
                SuppliersGrid.ItemsSource = _dbService.GetSuppliers();
                InventoryCategoriesGrid.ItemsSource = _dbService.GetInventoryCategories();
                EmployeesGrid.ItemsSource = _dbService.GetEmployees();

                StatusText.Text = $"Данные загружены: {DateTime.Now:HH:mm:ss}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}\n\nПроверьте подключение к БД!", "Ошибка");
                StatusText.Text = "Ошибка загрузки";
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadAllData();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AddEquipment_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddEquipmentWindow(_dbService);
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                LoadAllData();
                MainTabControl.SelectedIndex = 0;
            }
        }

        private void AddEquipmentCategory_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddSimpleWindow(_dbService, "EquipmentCategory");
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                LoadAllData();
                MainTabControl.SelectedIndex = 3;
            }
        }

        private void AddLocation_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddSimpleWindow(_dbService, "Location");
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                LoadAllData();
                MainTabControl.SelectedIndex = 3;
            }
        }

        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddSupplierWindow(_dbService);
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                LoadAllData();
                MainTabControl.SelectedIndex = 3;
            }
        }

        private void AddInventoryCategory_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddSimpleWindow(_dbService, "InventoryCategory");
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                LoadAllData();
                MainTabControl.SelectedIndex = 3;
            }
        }

        private void AddInventoryItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddInventoryItemWindow(_dbService);
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                LoadAllData();
                MainTabControl.SelectedIndex = 1;
            }
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddEmployeeWindow(_dbService);
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                LoadAllData();
                MainTabControl.SelectedIndex = 3;
            }
        }

        private void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddTransactionWindow(_dbService);
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                LoadAllData();
                MainTabControl.SelectedIndex = 2;
            }
        }
    }
}