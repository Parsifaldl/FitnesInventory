using System;
using System.Windows;
using System.Windows.Controls;
using FitnesInventory.Data;
using FitnesInventory.Models;

namespace FitnesInventory
{
	public partial class AddEditUserWindow : Window
	{
		private FitnesInventoryDbContext _context;
		private User _editingUser;
		private bool _isEditMode;

		public AddEditUserWindow(FitnesInventoryDbContext context, User user = null)
		{
			InitializeComponent();
			_context = context;
			_editingUser = user;
			_isEditMode = user != null;

			if (_isEditMode)
			{
				Title = "Редактирование пользователя";
				LoadUserData();

				UsernameBox.IsEnabled = false;
				if (user.Role == "Admin")
				{
					RoleBox.IsEnabled = false;
				}
			}
			else
			{
				Title = "Добавление пользователя";
				SetDefaultPermissions();
			}
		}

		private void LoadUserData()
		{
			UsernameBox.Text = _editingUser.Username;
			FullNameBox.Text = _editingUser.FullName;
			RoleBox.SelectedIndex = _editingUser.Role == "Admin" ? 1 : 0;

			CanViewEquipmentCheck.IsChecked = _editingUser.CanViewEquipment;
			CanAddEquipmentCheck.IsChecked = _editingUser.CanAddEquipment;
			CanViewInventoryCheck.IsChecked = _editingUser.CanViewInventory;
			CanAddInventoryCheck.IsChecked = _editingUser.CanAddInventory;
			CanViewTransactionsCheck.IsChecked = _editingUser.CanViewTransactions;
			CanAddTransactionsCheck.IsChecked = _editingUser.CanAddTransactions;
			CanViewEmployeesCheck.IsChecked = _editingUser.CanViewEmployees;
			CanAddEmployeesCheck.IsChecked = _editingUser.CanAddEmployees;
			CanViewSuppliersCheck.IsChecked = _editingUser.CanViewSuppliers;
			CanAddSuppliersCheck.IsChecked = _editingUser.CanAddSuppliers;
			CanViewLocationsCheck.IsChecked = _editingUser.CanViewLocations;
			CanAddLocationsCheck.IsChecked = _editingUser.CanAddLocations;
			CanViewCategoriesCheck.IsChecked = _editingUser.CanViewCategories;
			CanAddCategoriesCheck.IsChecked = _editingUser.CanAddCategories;
		}

		private void SetDefaultPermissions()
		{
			CanViewEquipmentCheck.IsChecked = true;
			CanAddEquipmentCheck.IsChecked = false;
			CanViewInventoryCheck.IsChecked = true;
			CanAddInventoryCheck.IsChecked = false;
			CanViewTransactionsCheck.IsChecked = true;
			CanAddTransactionsCheck.IsChecked = false;
			CanViewEmployeesCheck.IsChecked = true;
			CanAddEmployeesCheck.IsChecked = false;
			CanViewSuppliersCheck.IsChecked = true;
			CanAddSuppliersCheck.IsChecked = false;
			CanViewLocationsCheck.IsChecked = true;
			CanAddLocationsCheck.IsChecked = false;
			CanViewCategoriesCheck.IsChecked = true;
			CanAddCategoriesCheck.IsChecked = false;

			RoleBox.SelectedIndex = 0;
		}

		private void SaveBtn_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(UsernameBox.Text))
			{
				MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (string.IsNullOrEmpty(FullNameBox.Text))
			{
				MessageBox.Show("Введите ФИО", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (!_isEditMode && string.IsNullOrEmpty(PasswordBox.Password))
			{
				MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (_isEditMode)
			{
				_editingUser.FullName = FullNameBox.Text;
				_editingUser.Role = (RoleBox.SelectedItem as ComboBoxItem)?.Content.ToString();
				_editingUser.CanViewEquipment = CanViewEquipmentCheck.IsChecked ?? false;
				_editingUser.CanAddEquipment = CanAddEquipmentCheck.IsChecked ?? false;
				_editingUser.CanViewInventory = CanViewInventoryCheck.IsChecked ?? false;
				_editingUser.CanAddInventory = CanAddInventoryCheck.IsChecked ?? false;
				_editingUser.CanViewTransactions = CanViewTransactionsCheck.IsChecked ?? false;
				_editingUser.CanAddTransactions = CanAddTransactionsCheck.IsChecked ?? false;
				_editingUser.CanViewEmployees = CanViewEmployeesCheck.IsChecked ?? false;
				_editingUser.CanAddEmployees = CanAddEmployeesCheck.IsChecked ?? false;
				_editingUser.CanViewSuppliers = CanViewSuppliersCheck.IsChecked ?? false;
				_editingUser.CanAddSuppliers = CanAddSuppliersCheck.IsChecked ?? false;
				_editingUser.CanViewLocations = CanViewLocationsCheck.IsChecked ?? false;
				_editingUser.CanAddLocations = CanAddLocationsCheck.IsChecked ?? false;
				_editingUser.CanViewCategories = CanViewCategoriesCheck.IsChecked ?? false;
				_editingUser.CanAddCategories = CanAddCategoriesCheck.IsChecked ?? false;

				if (!string.IsNullOrEmpty(PasswordBox.Password))
				{
					_editingUser.PasswordHash = PasswordBox.Password;
				}

				_context.Users.Update(_editingUser);
			}
			else
			{
				var user = new User
				{
					Username = UsernameBox.Text,
					PasswordHash = PasswordBox.Password,
					FullName = FullNameBox.Text,
					Role = (RoleBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
					CanViewEquipment = CanViewEquipmentCheck.IsChecked ?? false,
					CanAddEquipment = CanAddEquipmentCheck.IsChecked ?? false,
					CanViewInventory = CanViewInventoryCheck.IsChecked ?? false,
					CanAddInventory = CanAddInventoryCheck.IsChecked ?? false,
					CanViewTransactions = CanViewTransactionsCheck.IsChecked ?? false,
					CanAddTransactions = CanAddTransactionsCheck.IsChecked ?? false,
					CanViewEmployees = CanViewEmployeesCheck.IsChecked ?? false,
					CanAddEmployees = CanAddEmployeesCheck.IsChecked ?? false,
					CanViewSuppliers = CanViewSuppliersCheck.IsChecked ?? false,
					CanAddSuppliers = CanAddSuppliersCheck.IsChecked ?? false,
					CanViewLocations = CanViewLocationsCheck.IsChecked ?? false,
					CanAddLocations = CanAddLocationsCheck.IsChecked ?? false,
					CanViewCategories = CanViewCategoriesCheck.IsChecked ?? false,
					CanAddCategories = CanAddCategoriesCheck.IsChecked ?? false,
					CreatedAt = DateTime.Now
				};
				_context.Users.Add(user);
			}

			try
			{
				_context.SaveChanges();
				MessageBox.Show("Сохранено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
				DialogResult = true;
				Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
								MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}
	}
}