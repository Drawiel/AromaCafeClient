using System.Windows;
using System.Windows.Controls;

namespace AromaCafeCliente.Windows
{
    /// <summary>
    /// Lógica de interacción para GUI_WaitersTableBill.xaml
    /// </summary>
    public partial class GUI_WaitersTableBill : Page {
        public GUI_WaitersTableBill(int tableId) {
            InitializeComponent();
            LoadTableOrder();
        }

        private void LoadTableOrder()
        {

            //this.dataGridProducts.
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e) {

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            if(this.NavigationService != null) {
                this.NavigationService.Navigate(new GUI_HomeWaitress());
            }
        }
    }
}
