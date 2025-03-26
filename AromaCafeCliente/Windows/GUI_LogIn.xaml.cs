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
using AromaCafeCliente.AromaCafeService;
using AromaCafeCliente.Managers;
using AromaCafeCliente.Windows;

namespace AromaCafeCliente {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GUI_LogIn : Page {
        public GUI_LogIn() {
            InitializeComponent();
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            string employeeNumber = this.txtBoxEmployeeNumber.Text;
            string employeePassword = this.txtBoxEmployeePassword.Password;

            if (!string.IsNullOrEmpty(employeeNumber) && !string.IsNullOrEmpty(employeePassword))
            {
                Employee employee = EmployeeManager.ValidateCredentials(employeeNumber, employeePassword);
                if (employee.EmployeeId > 0)
                {
                    switch (employee.EmployeeType)
                    {
                        case "Mesero":
                            if (this.NavigationService != null)
                            {
                                this.NavigationService.Navigate(new GUI_Employees());
                            }
                            break;
                        case "Cajero":
                            if (this.NavigationService != null)
                            {
                                this.NavigationService.Navigate(new GUI_HomeCashier());
                            }
                            break;
                        case "Gerente":
                            if (this.NavigationService != null)
                            {
                                this.NavigationService.Navigate(new GUI_HomeManager());
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void CleanFields(object sender, RoutedEventArgs e)
        {
            this.txtBoxEmployeeNumber.Text = string.Empty;
            this.txtBoxEmployeePassword.Password = string.Empty;
        }
    }
}
