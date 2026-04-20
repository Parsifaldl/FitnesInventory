using FitnesInventory.Data;
using FitnesInventory.Models;
using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace FitnesInventory
{
    public partial class MainWindow : Window
    {
        private FitnesInventoryDbContext _context;
        private User _currentUser;

        public MainWindow(User currentUser)
        {
            InitializeComponent();
            _context = new FitnesInventoryDbContext();
            _currentUser = currentUser;
            ApplyPermissions();
            LoadAllData();

            this.Title = $"Фитнес Инвентарь - Учет (Пользователь: {_currentUser.FullName})";
        }

        private void ApplyPermissions()
        {
            if (_currentUser.Role == "Admin")
            {
                return;
            }

            if (!_currentUser.CanViewEquipment) EquipmentGrid.Visibility = Visibility.Collapsed;
            if (!_currentUser.CanViewInventory) InventoryGrid.Visibility = Visibility.Collapsed;
            if (!_currentUser.CanViewTransactions) TransactionsGrid.Visibility = Visibility.Collapsed;
            if (!_currentUser.CanViewEmployees) EmployeesGrid.Visibility = Visibility.Collapsed;
            if (!_currentUser.CanViewSuppliers) SuppliersGrid.Visibility = Visibility.Collapsed;
            if (!_currentUser.CanViewLocations) LocationsGrid.Visibility = Visibility.Collapsed;
            if (!_currentUser.CanViewCategories)
            {
                EquipmentCategoriesGrid.Visibility = Visibility.Collapsed;
                InventoryCategoriesGrid.Visibility = Visibility.Collapsed;
            }

            foreach (var item in MainMenu.Items)
            {
                if (item is MenuItem menuItem && menuItem.Header.ToString() == "Добавить")
                {
                    if (!_currentUser.CanAddEquipment) RemoveMenuItem(menuItem, "Оборудование");
                    if (!_currentUser.CanAddInventory) RemoveMenuItem(menuItem, "Инвентарь");
                    if (!_currentUser.CanAddTransactions) RemoveMenuItem(menuItem, "Транзакцию");
                    if (!_currentUser.CanAddEmployees) RemoveMenuItem(menuItem, "Сотрудника");
                    if (!_currentUser.CanAddSuppliers) RemoveMenuItem(menuItem, "Поставщика");
                    if (!_currentUser.CanAddLocations) RemoveMenuItem(menuItem, "Местоположение");
                    if (!_currentUser.CanAddCategories)
                    {
                        RemoveMenuItem(menuItem, "Категорию оборудования");
                        RemoveMenuItem(menuItem, "Категорию инвентаря");
                    }
                    break;
                }
            }
        }

        private MenuItem FindMenuItem(MenuItem parent, string header)
        {
            foreach (var item in parent.Items)
            {
                if (item is MenuItem mi)
                {
                    if (mi.Header.ToString() == header) return mi;
                    var found = FindMenuItem(mi, header);
                    if (found != null) return found;
                }
            }
            return null;
        }

        private void RemoveMenuItem(MenuItem parent, string header)
        {
            var item = FindMenuItem(parent, header);
            if (item != null) parent.Items.Remove(item);
        }

        private void LoadAllData()
        {
            try
            {
                if (_currentUser.CanViewEquipment)
                    EquipmentGrid.ItemsSource = _context.Equipment.ToList();
                if (_currentUser.CanViewInventory)
                    InventoryGrid.ItemsSource = _context.Inventory_Item.ToList();
                if (_currentUser.CanViewTransactions)
                    TransactionsGrid.ItemsSource = _context.Inventory_Transaction.ToList();
                if (_currentUser.CanViewCategories)
                {
                    EquipmentCategoriesGrid.ItemsSource = _context.Equipment_Category.ToList();
                    InventoryCategoriesGrid.ItemsSource = _context.Inventory_Category.ToList();
                }
                if (_currentUser.CanViewLocations)
                    LocationsGrid.ItemsSource = _context.Location.ToList();
                if (_currentUser.CanViewSuppliers)
                    SuppliersGrid.ItemsSource = _context.Supplier.ToList();
                if (_currentUser.CanViewEmployees)
                    EmployeesGrid.ItemsSource = _context.Employee.ToList();

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
            if (!_currentUser.CanAddEquipment && _currentUser.Role != "Admin")
            {
                MessageBox.Show("У вас нет прав на добавление оборудования", "Доступ запрещён");
                return;
            }
            var window = new AddEquipmentWindow(_context);
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                LoadAllData();
                MainTabControl.SelectedIndex = 0;
            }
        }

        private void AddEquipmentCategory_Click(object sender, RoutedEventArgs e)
        {
            if (!_currentUser.CanAddCategories && _currentUser.Role != "Admin")
            {
                MessageBox.Show("У вас нет прав на добавление категорий", "Доступ запрещён");
                return;
            }
            var window = new AddSimpleWindow(_context, "EquipmentCategory");
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                LoadAllData();
                MainTabControl.SelectedIndex = 3;
            }
        }

        private void AddLocation_Click(object sender, RoutedEventArgs e)
        {
            if (!_currentUser.CanAddLocations && _currentUser.Role != "Admin")
            {
                MessageBox.Show("У вас нет прав на добавление локаций", "Доступ запрещён");
                return;
            }
            var window = new AddSimpleWindow(_context, "Location");
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                LoadAllData();
                MainTabControl.SelectedIndex = 3;
            }
        }

        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (!_currentUser.CanAddSuppliers && _currentUser.Role != "Admin")
            {
                MessageBox.Show("У вас нет прав на добавление поставщиков", "Доступ запрещён");
                return;
            }
            var window = new AddSupplierWindow(_context);
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                LoadAllData();
                MainTabControl.SelectedIndex = 3;
            }
        }

        private void AddInventoryCategory_Click(object sender, RoutedEventArgs e)
        {
            if (!_currentUser.CanAddCategories && _currentUser.Role != "Admin")
            {
                MessageBox.Show("У вас нет прав на добавление категорий", "Доступ запрещён");
                return;
            }
            var window = new AddSimpleWindow(_context, "InventoryCategory");
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                LoadAllData();
                MainTabControl.SelectedIndex = 3;
            }
        }

        private void AddInventoryItem_Click(object sender, RoutedEventArgs e)
        {
            if (!_currentUser.CanAddInventory && _currentUser.Role != "Admin")
            {
                MessageBox.Show("У вас нет прав на добавление инвентаря", "Доступ запрещён");
                return;
            }
            var window = new AddInventoryItemWindow(_context);
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                LoadAllData();
                MainTabControl.SelectedIndex = 1;
            }
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (!_currentUser.CanAddEmployees && _currentUser.Role != "Admin")
            {
                MessageBox.Show("У вас нет прав на добавление сотрудников", "Доступ запрещён");
                return;
            }
            var window = new AddEmployeeWindow(_context);
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                LoadAllData();
                MainTabControl.SelectedIndex = 3;
            }
        }

        private void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (!_currentUser.CanAddTransactions && _currentUser.Role != "Admin")
            {
                MessageBox.Show("У вас нет прав на добавление транзакций", "Доступ запрещён");
                return;
            }
            var window = new AddTransactionWindow(_context);
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                LoadAllData();
                MainTabControl.SelectedIndex = 2;
            }
        }

        private void ExportEmployeesToExcel()
        {
            var employees = _context.Employee.ToList();

            if (employees == null || employees.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта: Сотрудники", "Предупреждение");
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel файлы (*.xlsx)|*.xlsx";
            saveDialog.FileName = $"Сотрудники_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            if (saveDialog.ShowDialog() != true)
                return;

            Excel.Application excelApp = new Excel.Application();
            excelApp.SheetsInNewWorkbook = 1;
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

            try
            {
                worksheet.Name = "Сотрудники";

                worksheet.Cells[1, 1] = "ID";
                worksheet.Cells[1, 2] = "Имя";
                worksheet.Cells[1, 3] = "Фамилия";
                worksheet.Cells[1, 4] = "Должность";

                int rowIndex = 2;
                foreach (var emp in employees)
                {
                    worksheet.Cells[rowIndex, 1] = emp.EmployeeId;
                    worksheet.Cells[rowIndex, 2] = emp.FirstName ?? "";
                    worksheet.Cells[rowIndex, 3] = emp.LastName ?? "";
                    worksheet.Cells[rowIndex, 4] = emp.Position ?? "";
                    rowIndex++;
                }

                worksheet.Columns.AutoFit();

                Excel.Range range = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[rowIndex - 1, 4]];
                range.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                workbook.SaveAs(saveDialog.FileName);
                workbook.Close();
                excelApp.Quit();

                MessageBox.Show($"Экспорт сотрудников завершён!\n{saveDialog.FileName}", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void ExportSuppliersToExcel()
        {
            var suppliers = _context.Supplier.ToList();

            if (suppliers == null || suppliers.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта: Поставщики", "Предупреждение");
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel файлы (*.xlsx)|*.xlsx";
            saveDialog.FileName = $"Поставщики_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            if (saveDialog.ShowDialog() != true)
                return;

            Excel.Application excelApp = new Excel.Application();
            excelApp.SheetsInNewWorkbook = 1;
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

            try
            {
                worksheet.Name = "Поставщики";

                worksheet.Cells[1, 1] = "ID";
                worksheet.Cells[1, 2] = "Название";
                worksheet.Cells[1, 3] = "Телефон";
                worksheet.Cells[1, 4] = "Email";
                worksheet.Cells[1, 5] = "Адрес";

                int rowIndex = 2;
                foreach (var sup in suppliers)
                {
                    worksheet.Cells[rowIndex, 1] = sup.SupplierId;
                    worksheet.Cells[rowIndex, 2] = sup.SupplierName ?? "";
                    worksheet.Cells[rowIndex, 3] = sup.ContactPhone ?? "";
                    worksheet.Cells[rowIndex, 4] = sup.ContactEmail ?? "";
                    worksheet.Cells[rowIndex, 5] = sup.Address ?? "";
                    rowIndex++;
                }

                worksheet.Columns.AutoFit();

                Excel.Range range = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[rowIndex - 1, 5]];
                range.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                workbook.SaveAs(saveDialog.FileName);
                workbook.Close();
                excelApp.Quit();

                MessageBox.Show($"Экспорт поставщиков завершён!\n{saveDialog.FileName}", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void ExportBothToExcel()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel файлы (*.xlsx)|*.xlsx";
            saveDialog.FileName = $"FitnessInventory_Employees_Suppliers_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            if (saveDialog.ShowDialog() != true)
                return;

            var employees = _context.Employee.ToList();
            var suppliers = _context.Supplier.ToList();

            Excel.Application excelApp = new Excel.Application();
            excelApp.SheetsInNewWorkbook = 2;
            Excel.Workbook workbook = excelApp.Workbooks.Add();

            try
            {
                Excel.Worksheet empSheet = (Excel.Worksheet)workbook.Sheets[1];
                empSheet.Name = "Сотрудники";
                FillEmployeeSheet(empSheet, employees);

                Excel.Worksheet supSheet = (Excel.Worksheet)workbook.Sheets[2];
                supSheet.Name = "Поставщики";
                FillSupplierSheet(supSheet, suppliers);

                workbook.SaveAs(saveDialog.FileName);
                workbook.Close();
                excelApp.Quit();

                MessageBox.Show($"Экспорт завершён!\nФайл с двумя листами:\n{saveDialog.FileName}", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void FillEmployeeSheet(Excel.Worksheet worksheet, List<Employee> employees)
        {
            worksheet.Cells[1, 1] = "ID";
            worksheet.Cells[1, 2] = "Имя";
            worksheet.Cells[1, 3] = "Фамилия";
            worksheet.Cells[1, 4] = "Должность";

            int row = 2;
            foreach (var emp in employees)
            {
                worksheet.Cells[row, 1] = emp.EmployeeId;
                worksheet.Cells[row, 2] = emp.FirstName ?? "";
                worksheet.Cells[row, 3] = emp.LastName ?? "";
                worksheet.Cells[row, 4] = emp.Position ?? "";
                row++;
            }

            worksheet.Columns.AutoFit();
        }

        private void FillSupplierSheet(Excel.Worksheet worksheet, List<Supplier> suppliers)
        {
            worksheet.Cells[1, 1] = "ID";
            worksheet.Cells[1, 2] = "Название";
            worksheet.Cells[1, 3] = "Телефон";
            worksheet.Cells[1, 4] = "Email";
            worksheet.Cells[1, 5] = "Адрес";

            int row = 2;
            foreach (var sup in suppliers)
            {
                worksheet.Cells[row, 1] = sup.SupplierId;
                worksheet.Cells[row, 2] = sup.SupplierName ?? "";
                worksheet.Cells[row, 3] = sup.ContactPhone ?? "";
                worksheet.Cells[row, 4] = sup.ContactEmail ?? "";
                worksheet.Cells[row, 5] = sup.Address ?? "";
                row++;
            }

            worksheet.Columns.AutoFit();
        }

        public void ExportEmployeesBtn_Click(object sender, RoutedEventArgs e)
        {
            ExportEmployeesToExcel();
        }

        public void ExportSuppliersBtn_Click(object sender, RoutedEventArgs e)
        {
            ExportSuppliersToExcel();
        }

        public void ExportBothBtn_Click(object sender, RoutedEventArgs e)
        {
            ExportBothToExcel();
        }

        public void ManageUsers_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser.Role != "Admin")
            {
                MessageBox.Show("Только администратор может управлять пользователями", "Доступ запрещён");
                return;
            }
            var window = new UserManagementWindow(_context, _currentUser);
            window.Owner = this;
            window.ShowDialog();
        }
    }
}