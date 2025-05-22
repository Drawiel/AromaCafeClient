using AromaCafeCliente.Managers;
using OfficeOpenXml;
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

namespace AromaCafeCliente.Windows
{
    /// <summary>
    /// Interaction logic for GUI_HomeCashier.xaml
    /// </summary>
    public partial class GUI_HomeCashier : Page
    {
        public GUI_HomeCashier()
        {
            InitializeComponent();
            LogOutPopupControl.LogOutSuccess += OnLogOutSuccess;
            LogOutPopupControl.Cancelled += OnLogOutCancelled;
            ExpensesPopupControl.Cancelled += OnExpenseCancelled;
        }

        private void DataGridUserSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            ValidationPopup.Visibility = Visibility.Visible;
        }

        private void OnLogOutSuccess(object sender, EventArgs e)
        {
            ValidationPopup.Visibility = Visibility.Hidden;

            if (NavigationService != null)
            {
                NavigationService.Navigate(new GUI_LogIn());
            }
        }

        private void OnLogOutCancelled(object sender, EventArgs e)
        {
            ValidationPopup.Visibility = Visibility.Hidden;
        }

        private void BtnExpenses_Click(object sender, RoutedEventArgs e)
        {
            ExpensesPopUp.Visibility = Visibility.Visible;
        }

        private void OnExpenseCancelled(object sender, EventArgs e)
        {
            ExpensesPopUp.Visibility = Visibility.Hidden;
        }

        private void NavigateTablesCashier(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService != null)
            {
                this.NavigationService.Navigate(new GUI_TablesCashier());
            }
        }

        private void BtnCashCount_Click(object sender, RoutedEventArgs e)
        {
            {
                try
                {
                    EPPlusLicense ePPlusLicense = new EPPlusLicense();
                    ePPlusLicense.SetNonCommercialPersonal("Zaid Alexis Vazquez Ramirez");

                    var products = SalesManager.GetFinancialReportByRange();
                    if (products == null || products.Count == 0)
                    {
                        MessageBox.Show("No se encontraron gastos ni ingresos para generar el reporte.");
                        return;
                    }

                    using (var package = new ExcelPackage())
                    {
                        var worksheet = package.Workbook.Worksheets.Add("Inventario");

                        worksheet.Cells[1, 1].Value = "Monto";
                        worksheet.Cells[1, 2].Value = "Movimiento";
                        worksheet.Cells[1, 3].Value = "Fecha";

                        int row = 2;
                        foreach (var p in products)
                        {
                            worksheet.Cells[row, 1].Value = p.Monto;
                            worksheet.Cells[row, 2].Value = p.Movimiento;
                            worksheet.Cells[row, 3].Value = p.Fecha;
                            row++;
                        }

                        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                        var downloadsFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                        var filePath = System.IO.Path.Combine(downloadsFolder, "Corte" + DateTime.Today.ToString("ddMMyy") + ".xlsx");
                        FileInfo excelFile = new FileInfo(filePath);
                        package.SaveAs(excelFile);

                        ConfirmationMessagePopupControl.SetMessage("Corte generado en:\n" + filePath);

                        MessageBox.Show("Corte generado en:\n" + filePath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el reporte: " + ex.Message);
                }
            }
        }
    }
}
