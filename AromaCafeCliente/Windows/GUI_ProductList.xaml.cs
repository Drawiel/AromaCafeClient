using AromaCafeCliente.AromaCafeService;
using AromaCafeCliente.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public GUI_ProductList() {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                List<Product> productsList = ProductManager.GetProductsList();

                var products = new ObservableCollection<Product>(productsList);
                var productsView = CollectionViewSource.GetDefaultView(productsList);

                DataGridProducts.ItemsSource = productsView;

            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un error recuperando los empleados");
            }
        }
    }
}
