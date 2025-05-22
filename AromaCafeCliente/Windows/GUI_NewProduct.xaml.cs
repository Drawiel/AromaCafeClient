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
using System.Windows.Media.Animation;
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
                ProductType = txtBoxNumber.Text,
            };

            ProductManager.AddProduct(updatedProduct);
            ConfirmationMessagePopupControl.SetMessage("Se ha registrado con éxito el nuevo producto.");
            ConfirmationPopup.Visibility = Visibility.Visible;
            Storyboard fadeIn = (Storyboard)FindResource("FadeInStoryboard");
            fadeIn.Begin();
            this.NavigationService.Navigate(new GUI_ProductList());
        }
        private void FadeOutStoryboard_Completed(object sender, EventArgs e) {
            ConfirmationPopup.Visibility = Visibility.Hidden;
            ErrorPopup.Visibility = Visibility.Hidden;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
                this.NavigationService.Navigate(new GUI_ProductList());
        }

        private void LogOut_Click_1(object sender, RoutedEventArgs e)
        {

            ValidationPopup.Visibility = Visibility.Visible;
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService != null)
            {
                this.NavigationService.Navigate(new GUI_ProductList());
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Navigate(new GUI_HomeManager());
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService != null)
            {
                this.NavigationService.Navigate(new GUI_Employees());
            }
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService != null)
            {
                this.NavigationService.Navigate(new GUI_Reports());
            }
        }
    }
}
