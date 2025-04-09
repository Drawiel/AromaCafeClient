using AromaCafeCliente.AromaCafeService;
using AromaCafeCliente.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for GUI_ProductList.xaml
    /// </summary>
    public partial class GUI_ProductList : Page {
        private ObservableCollection<Product> products;
        private ICollectionView productsView;
        public GUI_ProductList() {
            InitializeComponent();
            LoadProducts();
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

        private void LoadProducts()
        {
            try
            {
                List<Product> productsList = ProductManager.GetProductsList();

                products = new ObservableCollection<Product>(productsList);
                productsView = CollectionViewSource.GetDefaultView(products);
                productsView.Filter = ProductFilter;
                DataGridProducts.ItemsSource = productsView;

            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un error recuperando los empleados");
            }
        }
        private void TxtSearchBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (productsView != null)
            {
                productsView.Refresh();
            }
        }

        private bool ProductFilter(object item)
        {
            if (string.IsNullOrEmpty(txtSearchBox.Text)) return true;

            var product = item as Product;
            if (product == null) return false;
            string searchText = txtSearchBox.Text.ToLower();

            return product.ProductName.ToLower().Contains(searchText);
        }

        private void DataGridProductSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProduct = DataGridProducts.SelectedItem as Product;

            if (productsView != null)
            {
                this.NavigationService.Navigate(new GUI_ProductDetails(selectedProduct.ProductId));
            }
        }
    }
}
