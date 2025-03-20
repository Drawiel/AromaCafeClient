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

namespace AromaCafeCliente.Windows {
    /// <summary>
    /// Interaction logic for GUI_EmployeeUpdate.xaml
    /// </summary>
    public partial class GUI_EmployeeUpdate : Page
    {

        private string employeType = " ";

        public GUI_EmployeeUpdate() {
            InitializeComponent();
            radioButtonCashier.Checked += RadioButtonCheckedChanged;
            radioButtonWaitress.Checked += RadioButtonCheckedChanged;
            radioButtonManager.Checked += RadioButtonCheckedChanged;
        }

        private void RadioButtonCheckedChanged(object sender, EventArgs e) {
            RadioButton radioButton = sender as RadioButton;
            employeType = radioButton.Tag.ToString();
        }

        private bool CheckAllFields() {
            TextBox[] textBoxes = {
                TxtBoxName,
                txtBoxLastName,
                txtBoxEmail,
                txtBoxUser,
                txtBoxStreet,
                txtBoxCity,
                txtBoxCP,
                txtBoxNumber,
            };

            return textBoxes.All(tb => !string.IsNullOrWhiteSpace(tb.Text));
        }

        private bool IsValidEmail() {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s+$";
            return Regex.IsMatch(emailPattern, txtBoxEmail.Text);
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e) {

            if (CheckAllFields()) {
                MessageBox.Show("Se ha actualizado correctamente la informacion del empleado");
            } else if (!CheckAllFields() || !IsValidEmail()){
                MessageBox.Show("Se han encontrado campos vacios o invalidos, favor de revisar");
            }

            radioButtonCashier.IsEnabled = false;
            radioButtonWaitress.IsEnabled = false;
            radioButtonManager.IsEnabled = false;
            btnSave.IsEnabled = false;
        }

        private void BtnEditClick(object sender, RoutedEventArgs e) {
            TextBox[] textBoxes = {
                TxtBoxName,
                txtBoxLastName,
                txtBoxEmail,
                txtBoxUser,
                txtBoxStreet,
                txtBoxCity,
                txtBoxCP,
                txtBoxNumber,
            };

            textBoxes.All(tb => tb.IsEnabled=true);
            radioButtonCashier.IsEnabled = true;
            radioButtonWaitress.IsEnabled = true;
            radioButtonManager.IsEnabled = true;
            btnSave.IsEnabled = true;
        }

        /*private bool UpdateProfile(Employee employee) { 
            try {
                int profileUpdated = userManager.UpdateProfile(employee);
                if(profileUpdates != -1){
                    return true;
                } else {
                    return false;
                }
            } catch (Exception){ 
                return false;
            }
        }

        private Employee CreateEmployee() {
            var updatedEmployee = new Employee {
                Name = TxtBoxName.Text,
                LastName = txtBoxLastName.Text,
                Email = txtBoxEmail.Text,
                Username = txtBoxUser.Text,
                PostalCode = txtBoxCP.Text,
                EmployeeAddress = txtBoxStreet.Text + ", " + txtBoxCity.Text,
                EmployeeType = employeType
            };
        }*/

        
    }
}
