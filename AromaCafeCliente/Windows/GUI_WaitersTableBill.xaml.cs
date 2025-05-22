using AromaCafeCliente.AromaCafeService;
using AromaCafeCliente.Helpers;
using AromaCafeCliente.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AromaCafeCliente.Windows
{
    /// <summary>
    /// Lógica de interacción para GUI_WaitersTableBill.xaml
    /// </summary>
    public partial class GUI_WaitersTableBill : Page {

        private int tableId;
        private List<Product> productsAvailable;
        private List<ProductOrder> productsOrdered;
        private ObservableCollection<OrderItemViewModel> orderItems;
        private ObservableCollection<ProductViewModel> gridItems;
        public GUI_WaitersTableBill(int tableId) {
            InitializeComponent();
            this.tableId = tableId;
            LoadProductList();
            PaintProductsAvailable();
            PaintTableOrder();
            PaintNewProducts();
        }

        private void PaintNewProducts()
        {
            dataGridNewProducts.ItemsSource = orderItems;
        }

        private void LoadProductList()
        {
            productsAvailable = ProductManager.GetProductsList();
            productsOrdered = OrderManager.GetOrdersByTable(tableId);
            orderItems = new ObservableCollection<OrderItemViewModel>();
        }

        private void PaintProductsAvailable(string category = null)
        {
            var filtered = string.IsNullOrEmpty(category)
                ? productsAvailable
                : productsAvailable.Where(p => p.Category == category);

            gridItems = new ObservableCollection<ProductViewModel>(
                filtered.Select(p => new ProductViewModel(
                    p.ProductName,
                    p.UnitPrice))
                );
            dataGridGroupProduct.ItemsSource = gridItems;
        }

        private void PaintTableOrder()
        {
            var gridItems = productsOrdered.Select(p => new ProductOrderViewModel(
                p.ProductName,
                p.Quantity,
                p.OrderState
                ))
            .ToList();
            this.dataGridProducts.ItemsSource = new ObservableCollection<ProductOrderViewModel>(gridItems);
        }

        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            var category = (sender as Button)?.Tag as string;
            PaintProductsAvailable(category);
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {


        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            if(this.NavigationService != null) {
                this.NavigationService.Navigate(new GUI_HomeWaitress());
            }
        }

        private void AddProductToOrder(object sender, RoutedEventArgs e)
        {

            var selected = dataGridGroupProduct.SelectedItem as ProductViewModel;
            if (selected == null) return;

            var existing = orderItems.FirstOrDefault(x => x.Producto == selected.Producto);
            if (existing != null)
            {
                existing.Cantidad++;
            }
            else
            {
                orderItems.Add(new OrderItemViewModel(
                    selected.Producto,
                    1,
                    selected.Precio ?? 0
                    ));
            }
            PaintNewProducts();
        }

        private void SubstractItemBeforeOrdering(object sender, RoutedEventArgs e)
        {
            var selected = dataGridNewProducts.SelectedItem as OrderItemViewModel;
            if (selected == null) return;

            if (selected.Cantidad == 1)
            {
                orderItems.Remove(selected);
            }
            else
            {
                selected.Cantidad--;
            }
            PaintNewProducts();
        }

        private void CloseTable_BtnClick(object sender, RoutedEventArgs e)
        {
            int closed = TableManager.CloseTable(tableId);
            if (closed == 1)
            {
                //mensaje de confirmacion mesa cerrada con exito
                if (NavigationService != null)
                {
                    NavigationService.Navigate(new GUI_HomeWaitress());
                }
            }
            else
            {
                //mensaje de error no se pudo cerrar la mesa
            }
        }

        private void OrderMarkedAsDelivered(object sender, RoutedEventArgs e)
        {
            var viewModel = (sender as CheckBox)?.DataContext as ProductOrderViewModel;

            if (viewModel == null) return;

            bool delivered = viewModel.Seleccionado;
            string productOrderName = viewModel.Producto;

            if (delivered && !string.IsNullOrEmpty(productOrderName))
            {
                int marked = OrderManager.MarkOrderAsDelivered(productOrderName, tableId);
                if (marked == 1)
                {
                    //confirmationmessage pedido entregado
                }
                else
                {
                    //mensaje de error error al entregar pedido
                }
            }
        }

        private void OrderMarkedAsRequested(object sender, RoutedEventArgs e)
        {
            var viewModel = (sender as CheckBox)?.DataContext as ProductOrderViewModel;

            if (viewModel == null) return;

            bool delivered = viewModel.Seleccionado;
            string productOrderName = viewModel.Producto;

            if (delivered && !string.IsNullOrEmpty(productOrderName))
            {
                int marked = OrderManager.MarkOrderAsRequested(productOrderName, tableId);
                if (marked == 1)
                {
                    //confirmationmessage pedido solicitado
                }
                else
                {
                    //mensaje de error error al solicitar el pedido
                }
            }
        }
    }
}
