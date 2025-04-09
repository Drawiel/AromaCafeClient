using AromaCafeCliente.Managers;
using System.IO;
using System;
using System.Windows;
using System.Windows.Controls;
using OfficeOpenXml;


namespace AromaCafeCliente.Windows
{
    /// <summary>
    /// Lógica de interacción para GUI_Reports.xaml
    /// </summary>
    public partial class GUI_Reports : Page {
        public GUI_Reports() {
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

        private void GenerateInventoryReport(object sender, RoutedEventArgs e)
        {
            try
            {
                var products = ProductManager.GetProductsList();
                if (products == null || products.Count == 0)
                {
                    MessageBox.Show("No se encontraron productos para generar el reporte.");
                    return;
                }

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Inventario");

                    worksheet.Cells[1, 1].Value = "ID Producto";
                    worksheet.Cells[1, 2].Value = "Nombre";
                    worksheet.Cells[1, 3].Value = "Descripción";
                    worksheet.Cells[1, 4].Value = "Precio Unitario";
                    worksheet.Cells[1, 5].Value = "Stock";
                    worksheet.Cells[1, 6].Value = "Tipo Producto";
                    worksheet.Cells[1, 7].Value = "Categoría";

                    int row = 2;
                    foreach (var p in products)
                    {
                        worksheet.Cells[row, 1].Value = p.ProductId;
                        worksheet.Cells[row, 2].Value = p.ProductName;
                        worksheet.Cells[row, 3].Value = p.Description;
                        worksheet.Cells[row, 4].Value = p.UnitPrice;
                        worksheet.Cells[row, 5].Value = p.Stock;
                        worksheet.Cells[row, 6].Value = p.ProductType;
                        row++;
                    }

                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    var downloadsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                    var filePath = Path.Combine(downloadsFolder, "InventoryReport.xlsx");
                    FileInfo excelFile = new FileInfo(filePath);
                    package.SaveAs(excelFile);

                    MessageBox.Show("Reporte de inventario generado en: " + filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message);
            }
        }
    }
}
