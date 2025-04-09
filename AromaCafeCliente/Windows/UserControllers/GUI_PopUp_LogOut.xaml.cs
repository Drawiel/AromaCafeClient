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
    /// Lógica de interacción para GUI_PopUp_LogOut.xaml
    /// </summary>
    public partial class GUI_PopUp_LogOut : UserControl {
        public event EventHandler LogOutSuccess;
        public event EventHandler Cancelled;
        public GUI_PopUp_LogOut() {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Cancelled?.Invoke(this, EventArgs.Empty);
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e) {
            string password = pswdBoxEmployeePassword.Password;
            if (!string.IsNullOrEmpty(password))
            {
                if (EmployeeManager.LogOut(password) == 1)
                {
                    LogOutSuccess?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("Error al cerrar la sesión");
                }
            }
            else
            {
                MessageBox.Show("Clave de acceso vacía");
            }
        }
    }
}
