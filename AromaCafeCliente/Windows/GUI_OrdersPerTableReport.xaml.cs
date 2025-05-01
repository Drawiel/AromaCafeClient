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
    /// Lógica de interacción para GUI_OrdersPerTableReport.xaml
    /// </summary>
    public partial class GUI_OrdersPerTableReport : Page {
        public GUI_OrdersPerTableReport() {
            InitializeComponent();
            LogOutPopupControl.LogOutSuccess += OnLogOutSuccess;
            LogOutPopupControl.Cancelled += OnLogOutCancelled;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e) {
            ValidationPopup.Visibility = Visibility.Visible;
        }

        private void OnLogOutSuccess(object sender, EventArgs e) {
            ValidationPopup.Visibility = Visibility.Hidden;
            NavigationService?.Navigate(new GUI_LogIn());
        }

        private void OnLogOutCancelled(object sender, EventArgs e) {
            ValidationPopup.Visibility = Visibility.Hidden;
        }
        private void NavigateHome(object sender, RoutedEventArgs e) {
            if(NavigationService != null) {
                NavigationService.Navigate(new GUI_HomeManager());
            }
        }

        private void NavigateEmployees(object sender, RoutedEventArgs e) {
            if(this.NavigationService != null) {
                this.NavigationService.Navigate(new GUI_Employees());
            }
        }

        private void NavigateProductList(object sender, RoutedEventArgs e) {
            if(this.NavigationService != null) {
                NavigationService.Navigate(new GUI_ProductList());
            }
        }

        private void NavigateReports(object sender, RoutedEventArgs e) {
            if(this.NavigationService != null) {
                NavigationService.Navigate(new GUI_Reports());
            }
        }
    }
}
