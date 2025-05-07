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
    /// Lógica de interacción para GUI_TablesCashier.xaml
    /// </summary>
    public partial class GUI_TablesCashier : Page {
        public GUI_TablesCashier() {
            InitializeComponent();
            LogOutPopupControl.LogOutSuccess += OnLogOutSuccess;
            LogOutPopupControl.Cancelled += OnLogOutCancelled;
            ExpensesPopupControl.Cancelled += OnExpenseCancelled;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            ValidationPopup.Visibility = Visibility.Visible;
        }

        private void OnLogOutSuccess(object sender, EventArgs e)
        {
            ValidationPopup.Visibility = Visibility.Hidden;
            NavigationService?.Navigate(new GUI_LogIn());
        }

        private void OnLogOutCancelled(object sender, EventArgs e)
        {
            ValidationPopup.Visibility = Visibility.Hidden;
        }

        private void BtnClickBefore(object sender, RoutedEventArgs e) {

        }

        private void NavigateHomeCashier(object sender, RoutedEventArgs e) {
            if(this.NavigationService != null) {
                this.NavigationService.Navigate(new GUI_HomeCashier());
            }
        }

        private void NavigateTablesCashier(object sender, RoutedEventArgs e) {
            if(this.NavigationService != null) {
                this.NavigationService.Navigate(new GUI_TablesCashier());
            }
        }

        private void BtnCashCount_Click(object sender, RoutedEventArgs e) {

        }

        private void BtnExpenses_Click(object sender, RoutedEventArgs e) {
            ExpensesPopUp.Visibility = Visibility.Visible;
        }

        private void OnExpenseCancelled(object sender, EventArgs e) {
            ExpensesPopUp.Visibility = Visibility.Hidden;
        }
    }
}
