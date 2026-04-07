using FitnesInventory.Data;
using FitnesInventory.Models;
using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;

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

        private void ExportEmployeesToExcel()
        {
            var employees = _dbService.GetEmployees().ToList();

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
            var suppliers = _dbService.GetSuppliers().ToList();

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

            var employees = _dbService.GetEmployees().ToList();
            var suppliers = _dbService.GetSuppliers().ToList();

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
    }
}