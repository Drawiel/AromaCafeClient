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
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            string employeeNumber = this.txtBoxEmployeeNumber.Text;
            string employeePassword = this.txtBoxEmployeePassword.Password;

            if (employeeNumber != null && employeePassword != null)
            {
                Employee employee = EmployeeManager.ValidateCredentials(employeeNumber, employeePassword);
                if (employee.EmployeeId > 0)
                {
                    switch (employee.EmployeeType)
                    {
                        case "Mesero":
                            /*GUI_Employees gUI_Employees = new GUI_Employees();
                            gUI_Employees.Show();
                            this.Close();*/
                            break;
                        case "Cajero":
                            /*GUI_HomeCashier gUI_HomeCashier = new GUI_HomeCashier();
                            gUI_HomeCashier.Show();
                            this.Close();*/
                            break;
                        case "Gerente":
                            /*GUI_HomeManager gUI_HomeManager = new GUI_HomeManager();
                            gUI_HomeManager.Show();
                            this.Close();*/
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
