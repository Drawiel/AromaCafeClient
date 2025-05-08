using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Lógica de interacción para GUI_CancelledOrdersByTableReport.xaml
    /// </summary>
    public partial class GUI_CancelledOrdersByTableReport : Page {
        public GUI_CancelledOrdersByTableReport() {
            InitializeComponent();
            LogOutPopupControl.LogOutSuccess += OnLogOutSuccess;
            LogOutPopupControl.Cancelled += OnLogOutCancelled;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e) {
            ValidationPopup.Visibility = Visibility.Visible;
        }

        private void OnLogOutSuccess(object sender, EventArgs e) {
            ValidationPopup.Visibility = Visibility.Hidden;
            NavigationService?.Navigate(new GUI_LogIn());
        }

        private void OnLogOutCancelled(object sender, EventArgs e) {
            ValidationPopup.Visibility = Visibility.Hidden;
        }
        private void NavigateHome(object sender, RoutedEventArgs e) {
            if(NavigationService != null) {
                NavigationService.Navigate(new GUI_HomeManager());
            }
        }

        private void NavigateEmployees(object sender, RoutedEventArgs e) {
            if(this.NavigationService != null) {
                this.NavigationService.Navigate(new GUI_Employees());
            }
        }

        private void NavigateProductList(object sender, RoutedEventArgs e) {
            if(this.NavigationService != null) {
                NavigationService.Navigate(new GUI_ProductList());
            }
        }

        private void NavigateReports(object sender, RoutedEventArgs e) {
            if(this.NavigationService != null) {
                NavigationService.Navigate(new GUI_Reports());
            }
        }

        private void DataGridUser_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            CreateTable();
        }

        private void FadeOutStoryboard_Completed(object sender, EventArgs e) {
            ConfirmationPopup.Visibility = Visibility.Hidden;
        }

        private void DataGridOrder_ColumnHeaderDragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e) {
            CreateTable();
        }

        public void CreateTable() {
            if (dataGridOrder.ItemsSource != null) {
                var items = dataGridOrder.ItemsSource.Cast<object>().ToList();

                if (items.Count == 0) {
                    ErrorMessagePopupControl.SetMessage("No hay datos para crear una tabla");
                    ErrorPopup.Visibility = Visibility.Visible;
                    Storyboard fadeIn = (Storyboard)FindResource("FadeInStoryboard");
                    fadeIn.Begin();
                    return;
                }

                // Crear un SaveFileDialog para elegir la ubicación del archivo
                SaveFileDialog saveFileDialog = new SaveFileDialog {
                    Filter = "Archivo CSV (*.csv)|*.csv",
                    DefaultExt = ".csv"
                };

                if (saveFileDialog.ShowDialog() == true) {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName)) {
                        // Escribir encabezados
                        var headers = dataGridOrder.Columns.Select(c => c.Header.ToString());
                        writer.WriteLine(string.Join(",", headers));

                        // Escribir filas
                        foreach (var item in items) {
                            var rowValues = dataGridOrder.Columns
                                .Select(c => GetPropertyValue(item, c.SortMemberPath)?.ToString() ?? "");
                            writer.WriteLine(string.Join(",", rowValues));
                        }
                    }

                    ConfirmationMessagePopupControl.SetMessage("Reporte guardado y generado exitosament");
                    ConfirmationPopup.Visibility = Visibility.Visible;
                }
            } else {
                ErrorMessagePopupControl.SetMessage("No hay datos para crear una tabla");
                ErrorPopup.Visibility = Visibility.Visible;
                Storyboard fadeIn = (Storyboard)FindResource("FadeInStoryboard");
                fadeIn.Begin();
            }
        }
    }
}