using AromaCafeCliente.AromaCafeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace AromaCafeCliente.Windows.UserControllers {
    /// <summary>
    /// Lógica de interacción para GUI_PopUp_Bills.xaml
    /// </summary>
    public partial class GUI_PopUp_Bills : UserControl {
        private ExpenseManagerClient expenseManager;

        public GUI_PopUp_Bills() {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrWhiteSpace(txtBoxCuantity.Text) && string.IsNullOrEmpty(txtBoxReason.Text)) {
                MessageBox.Show("Se han encontrado campos vacios, favor de revisar");
                return;
            } else if (!IsDecimal()) {
                MessageBox.Show("No se pudo ingresar el monto, compruebe el formato: ####.##");
                return;
            }

            if (registerExpense(decimal.Parse(txtBoxCuantity.Text))) {
                MessageBox.Show("Se ha registrado correctamente el gasto");
            } else {
                MessageBox.Show("Hubo un error inesperado al intentar registrar el gasto");
            }
        }

        private bool registerExpense(decimal amount) {
            expenseManager = new ExpenseManagerClient();
            DateTime dateTime = DateTime.Now;
            var expense = new Expense {
                Amount = amount,
                DateTime = dateTime,
            };

            try {
                int expenseRegistered = expenseManager.RegisterExpense(expense);

                if (expenseRegistered != -1) {
                    return true;
                } else {
                    return false;
                }

            } catch (Exception) {
                return false;
            }
        }

        private bool IsDecimal() {
            string decimalPattern = @"^\d+(\.\d{1,2})?$";
            return Regex.IsMatch(decimalPattern, txtBoxCuantity.Text);
        }
    }
}
