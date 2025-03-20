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

namespace AromaCafeCliente.Windows {
    /// <summary>
    /// Interaction logic for GUI_Employees.xaml
    /// </summary>
    public partial class GUI_Employees : Page {
        
        //private ObservableCollection<Employee> employeesList;
        private ICollectionView employeesView;
        
        public GUI_Employees() {
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees() {
            try {
                /*employeeManager = new EmployeeManagerClient();
                Employee[] employees = employeeManager.GetAllEmployee();
                
                foreach (var employee in employees){
                    dataGridUsers.Rows.Add(employee.LastName, employee.Name, employee.EmployeeType, employee.EmployeeType);
                }

                employeesView = CollectionViewSource.GetDefaultView(employeesList);
                employeesView.Filter = EmployeeFilter;

                dataGridUser.ItemsSource = employeesView;
                 */
            } catch (Exception){
                MessageBox.Show("Ocurrio un error recuperando los empleados");
            }
        }

        private void TxtSearchBoxTextChanged(object sender, TextChangedEventArgs e) {
            if (employeesView != null) {
                employeesView.Refresh(); 
            }
        }

        /*private bool EmployeeFilter(object item) {
            if (string.IsNullOrEmpty(txtSearchBox.Text)) return true;

            var employee = item as Employee;
            string searchText = txtSearchBox.Text.ToLower();

            return employee.LastName.ToLower().Contains(searchText) ||
                   employee.Name.ToLower().Contains(searchText) ||
                   employee.Type.ToLower().Contains(searchText) ||
                   employee.Status.ToLower().Contains(searchText);
        }*/
    }
}
