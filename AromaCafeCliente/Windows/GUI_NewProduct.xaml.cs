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

namespace AromaCafeCliente.Windows {
    /// <summary>
    /// Interaction logic for GUI_NewProduct.xaml
    /// </summary>
    public partial class GUI_NewProduct : Page {
        public GUI_NewProduct() {
            InitializeComponent();
            LogOutPopupControl.LogOutSuccess += OnLogOutSuccess;
            LogOutPopupControl.Cancelled += OnLogOutCancelled;
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

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Product updatedProduct = new Product
            {
                ProductId = int.Parse(txtBoxCode.Text),
                ProductName = TxtBoxName.Text.Trim(),
                Description = txtBoxDesciption.Text.Trim(),
                Stock = string.IsNullOrWhiteSpace(txtBoxUnits.Text) ? null : (int?)int.Parse(txtBoxUnits.Text),
                ProductType = txtBoxNumber.SelectedValue?.ToString(),
            };

            ProductManager.AddProduct(updatedProduct);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
                this.NavigationService.Navigate(new GUI_ProductList());
        }
    }
}
