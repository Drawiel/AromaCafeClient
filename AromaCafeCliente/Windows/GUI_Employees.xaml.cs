using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using AromaCafeCliente.AromaCafeService;
using AromaCafeCliente.Managers;

namespace AromaCafeCliente.Windows {
    /// <summary>
    /// Interaction logic for GUI_Employees.xaml
    /// </summary>
    public partial class GUI_Employees : Page {
        
        private EmployeeManagerClient employeeManager;
        private ObservableCollection<EmployeeWithStatus> employeesList;
        private ICollectionView employeesView;
        
        public GUI_Employees() {
            InitializeComponent();
            LoadEmployees();
        }

        private void NavigateEmployeeRegistration(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService != null)
            {
                this.NavigationService.Navigate(new GUI_EmployeeRegistration());
            }
        }

        private void LoadEmployees() {
            try {
                employeeManager = new EmployeeManagerClient();
                Employee[] employees = employeeManager.GetAllEmployee();
                List<EmployeeWithStatus> employeesWithStatus = new List<EmployeeWithStatus>(); 

                foreach (Employee employee in employees) {

                    var eWithStatus = new EmployeeWithStatus {
                        EmployeeId = employee.EmployeeId,
                        Name = employee.Name,
                        LastName = employee.LastName,
                        EmployeeType = employee.EmployeeType,
                        Status = (employee.EmployeeType == "Gerente" || employee.EmployeeType == "Cajero" || employee.EmployeeType == "Mesero")
                    ? "Habilitado"
                    : "Inhabilitado"
                    };
                    employeesWithStatus.Add(eWithStatus);
                }

                employeesList = new ObservableCollection<EmployeeWithStatus>(employeesWithStatus);
                employeesView = CollectionViewSource.GetDefaultView(employeesList);
                employeesView.Filter = EmployeeFilter;

                dataGridUser.ItemsSource = employeesView;
                 
            } catch (Exception){
                MessageBox.Show("Ocurrio un error recuperando los empleados");
            }
        }

        private void TxtSearchBoxTextChanged(object sender, TextChangedEventArgs e) {
            if (employeesView != null) {
                employeesView.Refresh(); 
            }
        }

        private bool EmployeeFilter(object item) {
            if (string.IsNullOrEmpty(txtSearchBox.Text)) return true;

            var employee = item as EmployeeWithStatus;
            if (employee == null) return false;
            string searchText = txtSearchBox.Text.ToLower();

            return employee.LastName.ToLower().Contains(searchText) ||
                   employee.Name.ToLower().Contains(searchText) ||
                   employee.Status.ToLower().Contains(searchText) ||
                   employee.EmployeeType.ToLower().Contains(searchText);
        }

        private void DataGridUserSelectionChanged(object sender, SelectionChangedEventArgs e) {
            var selectedEmployee = dataGridUser.SelectedItem as EmployeeWithStatus;

            if (employeesView != null) {
                this.NavigationService.Navigate(new GUI_EmployeeUpdate(selectedEmployee.EmployeeId));
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService != null)
            {
                NavigationService.GoBack();
            }
        }
        private void HideLogOut(object sender, RoutedEventArgs e)
        {
            this.ValidationPopup.Visibility = Visibility.Hidden;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            this.ValidationPopup.Visibility = Visibility.Visible;
        }

        private void ValidateUserLogOut(object sender, RoutedEventArgs e)
        {
            string password = pswdBoxEmployeePassword.Password;
            if (password != string.Empty)
            {
                if (EmployeeManager.LogOut(password) == 1)
                {
                    if (this.NavigationService != null)
                    {
                        this.NavigationService.Navigate(new GUI_LogIn());
                    }
                }
                else
                {
                    //Alert
                }
            }
            else
            {
                //Alert
            }
        }

        private void NavigateProductList(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null)
            {
                NavigationService.Navigate(new GUI_ProductList());
            }
        }

        private class EmployeeWithStatus {
            public int EmployeeId { get; set;}
            public string Name { get ; set ; }
            public string LastName { get; set; }
            public string EmployeeType { get ; set ; }
            public string Status { get ; set ; }
        }
    }
}
