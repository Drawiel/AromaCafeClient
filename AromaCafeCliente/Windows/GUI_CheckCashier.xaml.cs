using AromaCafeCliente.AromaCafeService;
using AromaCafeCliente.Helpers;
using AromaCafeCliente.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AromaCafeCliente.Windows {
    /// <summary>
    /// Lógica de interacción para GUI_CheckCashier.xaml
    /// </summary>
    public partial class GUI_CheckCashier : Page {
        TableManagerClient tableManagerClient;
        private int tableId;
        private List<ProductOrder> productsOrdered;
        public GUI_CheckCashier(int tableId) {
            InitializeComponent();
            this.tableId = tableId;
            LogOutPopupControl.LogOutSuccess += OnLogOutSuccess;
            LogOutPopupControl.Cancelled += OnLogOutCancelled;
            ExpensesPopupControl.Cancelled += OnExpenseCancelled;
            PaymentMethodPopupControl.Cancelled += OnPaymentCancelled;
            LoadDataGridBill();
        }

        private void LoadDataGridBill()
        {
            productsOrdered = OrderManager.GetOrdersByTable(tableId);
            if (productsOrdered != null)
            {
                var gridItems = productsOrdered.Select(p => new ProductOrderViewModel(
                    p.ProductName,
                    p.Quantity,
                    p.Price
                    ))
                .ToList();
                this.dataGridBill.ItemsSource = new ObservableCollection<ProductOrderViewModel>(gridItems);
            }
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
            PaymentMethodPopup.Visibility = Visibility.Visible;
        }

        private void OnPaymentCancelled(object sender, EventArgs e) {
            PaymentMethodPopup.Visibility= Visibility.Hidden;
        }

        private void NavigateHome(object sender, RoutedEventArgs e) {
            if(NavigationService != null) {
                NavigationService.Navigate(new GUI_HomeCashier());
            }
        }

        private void NavigateTables(object sender, RoutedEventArgs e) {
            if(NavigationService != null) {
                NavigationService.Navigate(new GUI_TablesCashier());
            }
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e) {
            if(NavigationService != null) {
                NavigationService.Navigate(new GUI_TablesCashier());
            }
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
        private void FadeOutStoryboard_Completed(object sender, EventArgs e) {
            ConfirmationPopup.Visibility = Visibility.Hidden;
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

        private void ModifyOrder_BtnClick(object sender, EventArgs e)
        {
            var selected = dataGridBill.SelectedItem as ProductOrderViewModel;

            if (selected != null)
            {
                this.ValidationPopupModifyOrder.Visibility = Visibility.Visible;
            }
        }

        private void CloseModifyOrder(object sender, EventArgs e)
        {
            this.ValidationPopupModifyOrder.Visibility= Visibility.Hidden;
        }

        private void ModifyOrderQuantity(object sender, EventArgs e)
        {
            var selected = dataGridBill.SelectedItem as ProductOrderViewModel;
            string productName = selected.Producto;
            try
            {
                int quantity = int.Parse (txtBoxNewCuantity.Text);
                int edited = OrderManager.EditOrderQuantity(tableId, productName, quantity);
                if (edited == 1)
                {
                    ConfirmationMessagePopupControl.SetMessage("Pedido modificado con éxito.");
                    ConfirmationPopup.Visibility = Visibility.Visible;
                    Storyboard fadeIn = (Storyboard)FindResource("FadeInStoryboard");
                    fadeIn.Begin();
                    CloseModifyOrder(sender, e);
                }
            }
            catch (FormatException formatException)
            {
                //error message cantidad no se pudo modificar
                ErrorMessagePopupControl.SetMessage("Error al modificar el pedido.");
                ErrorPopup.Visibility = Visibility.Visible;
                Storyboard fadeIn = (Storyboard)FindResource("FadeInStoryboard");
                fadeIn.Begin();
            }
            catch (ArgumentNullException  argumentNullException)
            {
                // error message igual que el de arriba
                ErrorMessagePopupControl.SetMessage("Error al modificar el pedido.");
                ErrorPopup.Visibility = Visibility.Visible;
                Storyboard fadeIn = (Storyboard)FindResource("FadeInStoryboard");
                fadeIn.Begin();
            }
        }
    }
}
