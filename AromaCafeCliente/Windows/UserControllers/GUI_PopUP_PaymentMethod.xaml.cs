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
    /// Lógica de interacción para GUI_PopUP_PaymentMethod.xaml
    /// </summary>
    public partial class GUI_PopUP_PaymentMethod : UserControl {
        public event EventHandler Cancelled;
        public GUI_PopUP_PaymentMethod() {
            InitializeComponent();
        }

        private void btnAcceptPaymentMethod_Click(object sender, RoutedEventArgs e) {

        }

        private void btnCancelPaymentMethod_Click(object sender, RoutedEventArgs e) {
            Cancelled?.Invoke(this, EventArgs.Empty);
        }

    }
}
