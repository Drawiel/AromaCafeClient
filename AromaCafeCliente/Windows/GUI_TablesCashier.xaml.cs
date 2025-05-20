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
    /// Lógica de interacción para GUI_TablesCashier.xaml
    /// </summary>
    public partial class GUI_TablesCashier : Page {

        private List<TableCustomer> loadedTables = new List<TableCustomer>();
        private const int TablesPerPage = 14;
        private int currentPage = 0;
        public GUI_TablesCashier() {
            InitializeComponent();
            LoadCustomerTables();
            LogOutPopupControl.LogOutSuccess += OnLogOutSuccess;
            LogOutPopupControl.Cancelled += OnLogOutCancelled;
        }

        private void LoadCustomerTables()
        {
            try
            {
                loadedTables = TableManager.GetActiveAndClosedTables();
                currentPage = 0;
                DisplayTables();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar mesas: {ex.Message}");
            }
        }

        private void DisplayTables()
        {
            for (int i = 0; i < TablesPerPage; i++)
            {
                var btn = FindName($"btnTable{i + 1}") as Button;
                if (btn == null) continue;

                int index = currentPage * TablesPerPage + i;

                if (index < loadedTables.Count)
                {
                    var table = loadedTables[index];
                    btn.Content = $"{table.TableName}\n({table.NumberPeople} pers.)";
                    btn.Tag = table;
                    btn.Visibility = Visibility.Visible;
                    btn.ClearValue(Button.StyleProperty);
                    btn.Background = Brushes.LightGreen;
                    btn.BorderBrush = Brushes.DarkGreen;

                    if (table.TableStatus == "Cerrada")
                    {
                        btn.Background = Brushes.Gray;
                        btn.BorderBrush = Brushes.DarkGray;
                    }
                }
                else
                {
                    btn.Content = $"Mesa {index + 1}\n(Disponible)";
                    btn.Tag = null;
                    btn.Visibility = Visibility.Hidden;
                }
            }
        }

        private void BtnClickBefore(object sender, RoutedEventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                DisplayTables();
            }
        }

        private void BtnClickNext(object sender, RoutedEventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)loadedTables.Count / TablesPerPage);
            if (currentPage < totalPages - 1)
            {
                currentPage++;
                DisplayTables();
            }
        }

        private void BtnTable_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is TableCustomer table)
            {
                if (table.TableStatus != "Cerrada")
                {
                    NavigationService?.Navigate(new GUI_CheckCashier(table.TableId));
                }
                else
                {
                    MessageBox.Show("La mesa está cerrada y no se puede acceder.");
                }
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

        private void NavigateHomeCashier(object sender, RoutedEventArgs e) {
            if(this.NavigationService != null) {
                this.NavigationService.Navigate(new GUI_HomeCashier());
            }
        }

        private void NavigateTablesCashier(object sender, RoutedEventArgs e) {
            if(this.NavigationService != null) {
                this.NavigationService.Navigate(new GUI_TablesCashier());
            }
        }

        private void BtnCashCount_Click(object sender, RoutedEventArgs e) {

        }
    }
}
