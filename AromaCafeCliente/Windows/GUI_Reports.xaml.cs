using AromaCafeCliente.Managers;
using System.IO;
using System;
using System.Windows;
using System.Windows.Controls;
using OfficeOpenXml;
using System.Windows.Media.Animation;


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

        private void NavigateHome(object sender, RoutedEventArgs e) 
            {
            if(NavigationService != null) 
                {
                NavigationService.Navigate(new GUI_HomeManager());
            }
        }

        private void NavigateEmployees(object sender, RoutedEventArgs e) 
            {
            if(this.NavigationService != null) 
                {
                this.NavigationService.Navigate(new GUI_Employees());
            }
        }

        private void NavigateProductList(object sender, RoutedEventArgs e) 
            {
            if(this.NavigationService != null) 
                {
                NavigationService.Navigate(new GUI_ProductList());
            }
        }

        private void NavigateReports(object sender, RoutedEventArgs e) 
            {
            if(this.NavigationService != null) 
                {
                NavigationService.Navigate(new GUI_Reports());
            }
        }

        private void GenerateInventoryReport(object sender, RoutedEventArgs e)
        {
            try
            {
                EPPlusLicense ePPlusLicense = new EPPlusLicense();
                ePPlusLicense.SetNonCommercialPersonal("Zaid Alexis Vazquez Ramirez");

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
                    var filePath = Path.Combine(downloadsFolder, "InventoryReport"+DateTime.Today.ToString("ddMMyy")+".xlsx");
                    FileInfo excelFile = new FileInfo(filePath);
                    package.SaveAs(excelFile);

                    ConfirmationMessagePopupControl.SetMessage("Reporte de inventario generado en:\n" + filePath);
                    ConfirmationPopup.Visibility = Visibility.Visible;

                    Storyboard fadeIn = (Storyboard)FindResource("FadeInStoryboard");
                    fadeIn.Begin();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message);
            }
        }

        private void FadeOutStoryboard_Completed(object sender, EventArgs e) {
            ConfirmationPopup.Visibility = Visibility.Hidden;
        }
        private void DatePopupVisible_Click(object sender, EventArgs e)
        {
            this.ValidationPopupDate.Visibility = Visibility.Visible;
        }

        private void DatePopupHidden_Click(object sender, EventArgs e)
        {
            this.ValidationPopupDate.Visibility = Visibility.Hidden;
        }

        private void ExpensesPerDay_Click(object sender, RoutedEventArgs ev)
        {
            try
            {
                EPPlusLicense ePPlusLicense = new EPPlusLicense();
                ePPlusLicense.SetNonCommercialPersonal("Zaid Alexis Vazquez Ramirez");

                DateTime? selectedDate = datePicker.SelectedDate;
                if (!selectedDate.HasValue)
                {
                    MessageBox.Show("Selecciona una fecha válida.");
                    return;
                }

                var expenses = ExpenseManager.GetExpensesByDay(selectedDate.Value);

                if (expenses == null || expenses.Count == 0)
                {
                    MessageBox.Show("No se encontraron gastos para la fecha seleccionada.");
                    return;
                }

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Gastos del Día");

                    worksheet.Cells[1, 1].Value = "ID Gasto";
                    worksheet.Cells[1, 2].Value = "Fecha";
                    worksheet.Cells[1, 3].Value = "Monto";

                    int row = 2;
                    foreach (var e in expenses)
                    {
                        worksheet.Cells[row, 1].Value = e.ExpenseId;
                        worksheet.Cells[row, 2].Value = e.DateTime.ToString("yyyy-MM-dd");
                        worksheet.Cells[row, 3].Value = e.Amount;
                        row++;
                    }

                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    var downloadsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                    var fileName = $"Gastos_{selectedDate.Value:ddMMyy}.xlsx";
                    var filePath = Path.Combine(downloadsFolder, fileName);

                    package.SaveAs(new FileInfo(filePath));

                    ConfirmationMessagePopupControl.SetMessage("Reporte de gastos generado en:\n" + filePath);
                    ConfirmationPopup.Visibility = Visibility.Visible;

                    Storyboard fadeIn = (Storyboard)FindResource("FadeInStoryboard");
                    fadeIn.Begin();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message);
            }
        }

    }
}
