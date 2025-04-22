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
    /// Interaction logic for GUI_PopUp_UserStatus.xaml
    /// </summary>
    public partial class GUI_PopUp_UserStatus : UserControl {
        public event EventHandler Cancelled;
        public event EventHandler StatusChanged;
        public GUI_PopUp_UserStatus() {
            InitializeComponent();
        }
        public string SelectedStatus {
            get { return comboBoxStatus.Text; }  // O SelectedItem, dependiendo de tu binding
        }

        private void BtnAcceptClick(object sender, RoutedEventArgs e) {
            StatusChanged?.Invoke(this, EventArgs.Empty);
            Cancelled?.Invoke(this, EventArgs.Empty);
        }

        private void btnCancelStatus_Click(object sender, RoutedEventArgs e) {
            Cancelled?.Invoke(this, EventArgs.Empty);
        }
    }
}
