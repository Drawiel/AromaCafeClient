using AromaCafeCliente.AromaCafeService;
using AromaCafeCliente.Managers;
using AromaCafeCliente.Windows.UserControllers;
using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for GUI_ProductDetails.xaml
    /// </summary>
    public partial class GUI_ProductDetails : Page {
        int productId;
        public GUI_ProductDetails(int _productId) {
            InitializeComponent();
            productId = _productId;
            LoadProductInfo(_productId);
            LogOutPopupControl.LogOutSuccess += OnLogOutSuccess;
            LogOutPopupControl.Cancelled += OnLogOutCancelled;
        }

        private void LoadProductInfo(int productId)
        {
            Product product = ProductManager.GetProduct(productId);
            if(product != null)
            {
                TxtBoxName.Text = product.ProductName;
                txtBoxDesciption.Text = product.Description;
                if (product.Stock == null)
                {
                    txtBoxUnits.Text = "";
                }
                else
                {
                    txtBoxUnits.Text = product.Stock.ToString();
                }
                txtBoxCode.Text = product.ProductId.ToString();
                Console.Write(product.ProductType);
                txtBoxNumber.SelectedValue = product.ProductType;

            }
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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            txtBoxNumber.IsEnabled = true;
            comboBoxGroup.IsEnabled = true;
            TxtBoxName.IsEnabled = true;
            txtBoxDesciption.IsEnabled = true;
            txtBoxUnits.IsEnabled = true;
            txtBoxCode.IsEnabled = true;

            btnCancel.IsEnabled = true;
            btnSave.IsEnabled = true;

            btnStock.IsEnabled = false;
            btnEdit.IsEnabled = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
                Product updatedProduct = new Product
                {
                    ProductId = int.Parse(txtBoxCode.Text),
                    ProductName = TxtBoxName.Text.Trim(),
                    Description = txtBoxDesciption.Text.Trim(),
                    Stock = string.IsNullOrWhiteSpace(txtBoxUnits.Text) ? null : (int?)int.Parse(txtBoxUnits.Text),
                    ProductType = txtBoxNumber.Text,
                };

                ProductManager.UpdateProduct(updatedProduct);
            txtBoxNumber.IsEnabled = false;
            comboBoxGroup.IsEnabled = false;
            TxtBoxName.IsEnabled = false;
            txtBoxDesciption.IsEnabled = false;
            txtBoxUnits.IsEnabled = false;
            txtBoxCode.IsEnabled = false;

            btnCancel.IsEnabled = false;
            btnSave.IsEnabled = false;

            btnStock.IsEnabled = true;
            btnEdit.IsEnabled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            txtBoxNumber.IsEnabled = false;
            comboBoxGroup.IsEnabled = false;
            TxtBoxName.IsEnabled = false;
            txtBoxDesciption.IsEnabled = false;
            txtBoxUnits.IsEnabled = false;
            txtBoxCode.IsEnabled = false;

            LoadProductInfo(productId);

            btnCancel.IsEnabled = false;
            btnSave.IsEnabled = false;

            btnStock.IsEnabled = true;
            btnEdit.IsEnabled = true;
        }

        private void btnStock_Click(object sender, RoutedEventArgs e)
        {
            validationPopup.Visibility = Visibility.Visible;
        }

        private void BtnAcceptClick(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(Cantidad.Text, out int cantidad))
            {
                ProductManager.UpdateProductStock(productId, cantidad);
                MessageBox.Show("Se ha actualizado con éxito el producto.");
                validationPopup.Visibility = Visibility.Hidden;
            }
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            validationPopup.Visibility = Visibility.Hidden;
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
