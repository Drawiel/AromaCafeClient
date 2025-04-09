using AromaCafeCliente.AromaCafeService;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para GUI_CheckCashier.xaml
    /// </summary>
    public partial class GUI_CheckCashier : Page {
        TableManagerClient tableManagerClient;
        public GUI_CheckCashier() {
            InitializeComponent();
            LogOutPopupControl.LogOutSuccess += OnLogOutSuccess;
            LogOutPopupControl.Cancelled += OnLogOutCancelled;
            ExpensesPopupControl.Cancelled += OnExpenseCancelled;
        }

        private void DataGridUserSelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            ValidationPopup.Visibility = Visibility.Visible;
        }

        private void OnLogOutSuccess(object sender, EventArgs e)
        {
            ValidationPopup.Visibility = Visibility.Hidden;

            if (NavigationService != null)
            {
                NavigationService.Navigate(new GUI_LogIn());
            }
        }

        private void OnLogOutCancelled(object sender, EventArgs e)
        {
            ValidationPopup.Visibility = Visibility.Hidden;
        }

        private void BtnExpenses_Click(object sender, RoutedEventArgs e) {
            ExpensesPopUp.Visibility = Visibility.Visible;
        }

        private void OnExpenseCancelled(object sender, EventArgs e) {
            ExpensesPopUp.Visibility = Visibility.Hidden;
        }

        private void BtnCloseBill_Click(object sender, RoutedEventArgs e) {

        }

        private decimal TotalSum() {
            decimal total = 0;

            foreach (var item in dataGridBill.Items) {
                if (item is DataRowView row) {
                    if (decimal.TryParse(row["Precio"].ToString(), out decimal valor)) {
                        total += valor;
                    }
                }
            }

            return total;
        }

        private bool ChargeBill(string paymentType) { 
            tableManagerClient = new TableManagerClient();
            DateTime dateTime = DateTime.Now;
            decimal total = TotalSum();
            int tableId = 1;

            var newCharge = new Charge {
                Date = dateTime,
                TableId = tableId,
                TotalCharge = total,
                TypePayment = paymentType
            };

            try {
                int chargeRegistered = tableManagerClient.ChargeBill(newCharge);

                if (chargeRegistered != -1) {
                    return true;
                } else {
                    return false;
                }

            } catch (Exception) {
                return false;
            }
        }
    }
}
