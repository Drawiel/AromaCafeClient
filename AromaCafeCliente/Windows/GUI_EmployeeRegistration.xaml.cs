using AromaCafeCliente.AromaCafeService;
using AromaCafeCliente.Helpers;
using AromaCafeCliente.Managers;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for GUI_EmployeeRegistration.xaml
    /// </summary>
    public partial class GUI_EmployeeRegistration : Page {
        public GUI_EmployeeRegistration() {
            InitializeComponent();
        }

        private bool ValidateAllFields()
        {
            if (!TextValidator.ValidateNameInput(this.TxtBoxName.Text))
            {
                return false;
            }
            if (!TextValidator.ValidateNameInput(this.txtBoxLastName.Text))
            {
                return false;
            }
            if (!TextValidator.ValidateEmailInput(this.txtBoxEmail.Text))
            {
                return false;
            }
            if (!TextValidator.ValidatePostalCode(this.txtBoxNumber.Text))
            {
                return false;
            }
            if (!TextValidator.ValidateNotEmpty(this.txtBoxUser.Text))
            {
                return false;
            }
            if (!TextValidator.ValidateNotEmpty(this.txtBoxStreet.Text))
            {
                return false;
            }
            if ((chkBoxWaitress.IsChecked == false) && (chkBoxManager.IsChecked == false) && (chkBoxCashier.IsChecked == false))
            {
                return false;
            }
            return true;
        }

        private void RegistrateNewEmployee(object sender, RoutedEventArgs e)
        {
            if (ValidateAllFields())
            {
                Employee employee = CreateEmployee();
                string employeeRegistered = EmployeeManager.RegisterEmployee(employee);
                if (employeeRegistered != "error")
                {
                    Console.WriteLine("Creado");
                }
            }
        }

        private Employee CreateEmployee()
        {
            Employee employee = new Employee {
                Name = TxtBoxName.Text,
                LastName = txtBoxLastName.Text,
                Email = txtBoxEmail.Text,
                PostalCode = txtBoxNumber.Text,
                Username = txtBoxUser.Text,
                EmployeeType = chkBoxWaitress.IsChecked ?? false ? "Mesero" :
                   chkBoxCashier.IsChecked ?? false ? "Cajero" :
                   chkBoxManager.IsChecked ?? false ? "Gerente" : "Desconocido"
            };
            return employee;
        }

        private void NavigateEmployees(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService != null)
            {
                this.NavigationService.Navigate(new GUI_Employees());
            }
        }
    }
}
