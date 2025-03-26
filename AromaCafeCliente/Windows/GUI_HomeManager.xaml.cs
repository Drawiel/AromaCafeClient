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
    /// Interaction logic for GUI_HomeManager.xaml
    /// </summary>
    public partial class GUI_HomeManager : Page {
        public GUI_HomeManager() {
            InitializeComponent();
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

        private void HideLogOut(object sender, RoutedEventArgs e)
        {
            this.ValidationPopup.Visibility = Visibility.Hidden;
        }

        private void UsersClick(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new GUI_Employees());
        }
    }
}
