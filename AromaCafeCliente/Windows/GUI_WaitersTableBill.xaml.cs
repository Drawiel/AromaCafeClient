using AromaCafeCliente.AromaCafeService;
using AromaCafeCliente.Helpers;
using AromaCafeCliente.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

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
            List<ProductOrder> completeOrder = orderItems.Select(o => new ProductOrder
            {
                ProductName = o.Producto,
                Quantity = o.Cantidad
            }).ToList();
            OrderManager.SendOrder(tableId, completeOrder);
            MessageBox.Show("Se ha realizado el pedido con  éxito.");
            orderItems = new ObservableCollection<OrderItemViewModel>();
            PaintNewProducts();
            LoadProductList();
            PaintTableOrder();
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
            string productName = selected.Producto;

            if (selected.Cantidad == 1)
            {
                orderItems.Remove(selected);
                int marked = OrderManager.MarkOrderAsCancelled(productName, tableId);
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
                ConfirmationMessagePopupControl.SetMessage("Mesa cerrada con exito.");
                ConfirmationPopup.Visibility = Visibility.Visible;
                Storyboard fadeIn = (Storyboard)FindResource("FadeInStoryboard");
                fadeIn.Begin();
                if (NavigationService != null)
                {
                    NavigationService.Navigate(new GUI_HomeWaitress());
                }
            }
            else
            {
                ErrorMessagePopupControl.SetMessage("No se pudo cerrar la mesa.");
                ErrorPopup.Visibility = Visibility.Visible;
                Storyboard fadeIn = (Storyboard)FindResource("FadeInStoryboard");
                fadeIn.Begin();
            }
        }

        private void FadeOutStoryboard_Completed(object sender, EventArgs e) {
            ConfirmationPopup.Visibility = Visibility.Hidden;
            ErrorPopup.Visibility = Visibility.Hidden;
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
                    ConfirmationMessagePopupControl.SetMessage("Pedido entregado.");
                    ConfirmationPopup.Visibility = Visibility.Visible;
                    Storyboard fadeIn = (Storyboard)FindResource("FadeInStoryboard");
                    fadeIn.Begin();
                }
                else
                {
                    ErrorMessagePopupControl.SetMessage("Error al entregar pedido.");
                    ErrorPopup.Visibility = Visibility.Visible;
                    Storyboard fadeIn = (Storyboard)FindResource("FadeInStoryboard");
                    fadeIn.Begin();
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
                    ConfirmationMessagePopupControl.SetMessage("Pedido solicitado con éxito.");
                    ConfirmationPopup.Visibility = Visibility.Visible;
                    Storyboard fadeIn = (Storyboard)FindResource("FadeInStoryboard");
                    fadeIn.Begin();
                }
                else
                {
                    ErrorMessagePopupControl.SetMessage("Error al solicitar pedido.");
                    ErrorPopup.Visibility = Visibility.Visible;
                    Storyboard fadeIn = (Storyboard)FindResource("FadeInStoryboard");
                    fadeIn.Begin();
                }
            }
        }
    }
}
