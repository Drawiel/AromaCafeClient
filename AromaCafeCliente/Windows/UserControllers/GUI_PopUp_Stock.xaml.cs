using AromaCafeCliente.AromaCafeService;
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

namespace AromaCafeCliente.Windows.UserControllers {
    /// <summary>
    /// Interaction logic for GUI_PopUp_Stock.xaml
    /// </summary>
    public partial class GUI_PopUp_Stock : UserControl {
        int productId;
        public event EventHandler Cancelled;
        public GUI_PopUp_Stock() {
            InitializeComponent();
        }
        private void BtnAcceptClick(object sender, RoutedEventArgs e) {
            if(int.TryParse(Cantidad.Text, out int cantidad)) {
                ProductManager.UpdateProductStock(productId, cantidad);
                MessageBox.Show("Se ha actualizado con éxito el producto.");
                Cancelled?.Invoke(this, EventArgs.Empty);
            }
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e) {
            Cancelled?.Invoke(this, EventArgs.Empty);
        }

    }
}
