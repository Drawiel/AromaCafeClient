using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
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
using AromaCafeCliente.AromaCafeService;

namespace AromaCafeCliente.Windows {
    /// <summary>
    /// Interaction logic for GUI_EmployeeUpdate.xaml
    /// </summary>
    public partial class GUI_EmployeeUpdate : Page
    {

        private string employeeType = " ";
        private int employeeId = 0;
        private string newAccessCode = " ";

        AromaCafeService.EmployeeManagerClient employeeManagerClient;

        public GUI_EmployeeUpdate(int employeeId) {
            InitializeComponent();
            LoadEmployeeData(employeeId);
            this.employeeId = employeeId;
            radioButtonCashier.Checked += RadioButtonCheckedChanged;
            radioButtonWaitress.Checked += RadioButtonCheckedChanged;
            radioButtonManager.Checked += RadioButtonCheckedChanged;
        }

        private void RadioButtonCheckedChanged(object sender, EventArgs e) {
            RadioButton radioButton = sender as RadioButton;
            employeeType = radioButton.Tag.ToString();
        }

        private bool CheckAllFields() {
            TextBox[] textBoxes = {
                txtBoxName,
                txtBoxLastName,
                txtBoxEmail,
                txtBoxUser,
                txtBoxStreet,
                txtBoxCP,
            };

            return textBoxes.All(tb => !string.IsNullOrWhiteSpace(tb.Text));
        }

        private bool IsValidEmail() {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(emailPattern, txtBoxEmail.Text);
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e) {

            bool updatedProfile = UpdateProfile(CreateEmployee());
            if (CheckAllFields() && updatedProfile) {
                MessageBox.Show("Se ha actualizado correctamente la informacion del empleado");
            } else if (!CheckAllFields() || !IsValidEmail()){
                MessageBox.Show("Se han encontrado campos vacios o invalidos, favor de revisar");
            } else if (!updatedProfile){
                MessageBox.Show("Hubo un error al actualizar el empleado");
            }

            radioButtonCashier.IsEnabled = false;
            radioButtonWaitress.IsEnabled = false;
            radioButtonManager.IsEnabled = false;
            btnSave.IsEnabled = false;
        }

        private void BtnEditClick(object sender, RoutedEventArgs e) {
            TextBox[] textBoxes = {
                txtBoxName,
                txtBoxLastName,
                txtBoxEmail,
                txtBoxUser,
                txtBoxStreet,
                txtBoxCP,
            };

            textBoxes.All(tb => tb.IsEnabled=true);
            radioButtonCashier.IsEnabled = true;
            radioButtonWaitress.IsEnabled = true;
            radioButtonManager.IsEnabled = true;
            btnSave.IsEnabled = true;
        }

        private bool UpdateProfile(Employee employee) {
            employeeManagerClient = new EmployeeManagerClient();
            try {
                int profileUpdated = employeeManagerClient.UpdateProfile(employee);
                if(profileUpdated != -1){
                    return true;
                } else {
                    return false;
                }
            } catch (Exception){ 
                return false;
            }
        }

        private Employee CreateEmployee() {
            SetEmployeeType();
            var updatedEmployee = new Employee {
                Name = txtBoxName.Text,
                LastName = txtBoxLastName.Text,
                Email = txtBoxEmail.Text,
                Username = txtBoxUser.Text,
                PostalCode = txtBoxCP.Text,
                EmployeeAddress = txtBoxStreet.Text,
                EmployeeType = employeeType,
                EmployeeId = employeeId
            };

            return updatedEmployee;
        }

        private void LoadEmployeeData(int employeeId) {
            employeeManagerClient = new EmployeeManagerClient();
            Employee employee = employeeManagerClient.GetEmployeeInformation(employeeId);
            try {
                txtBoxName.Text = employee.Name;
                txtBoxLastName.Text = employee.LastName;
                txtBoxEmail.Text = employee.Email;
                txtBoxUser.Text = employee.Username;
                txtBoxStreet.Text = employee.EmployeeAddress;
                txtBoxCP.Text = employee.PostalCode;
            } catch (Exception) {

            }

            if(employee.EmployeeType == "Gerente") {
                radioButtonManager.IsChecked = true;
            } else if (employee.EmployeeType == "Cajero") {
                radioButtonCashier.IsChecked = true;
            } else if (employee.EmployeeType == "Mesero") {
                radioButtonWaitress.IsChecked = true;
            } else if (employee.EmployeeType == "Inhabilitado") {

            }
        }

        private void SetEmployeeType() {
            if (comboBoxStatus.Text == "Deshabilitado") {
                employeeType = "Deshabilitado";
            } else if (radioButtonCashier.IsChecked == true) {
                employeeType = "Cajero";
            } else if (radioButtonWaitress.IsChecked == true) {
                employeeType = "Mesero";
            } else if (radioButtonManager.IsChecked == true) {
                employeeType = "Gerente";
            }
        }

        private void BtnStatusClick(object sender, RoutedEventArgs e) {
            validationPopup.Visibility = Visibility.Visible;
        }

        private void BtnStatusCancelClick(object sender, RoutedEventArgs e) {
            validationPopup.Visibility = Visibility.Collapsed;
        }

        private void BtnAcceptClick(object sender, RoutedEventArgs e) {
            validationPopup.Visibility = Visibility.Collapsed;
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new GUI_Employees());
        }

        private void BtnChangeAccessCodeClick(object sender, RoutedEventArgs e) {
            bool updatedAccessCodeProfile = UpdateAccessCodeProfile(CreateNewAccessCode());
            if (updatedAccessCodeProfile) {
                MessageBox.Show("Se ha creado correctamente la nueva clave de acceso: "+ newAccessCode);
            } else if (!updatedAccessCodeProfile) {
                MessageBox.Show("Hubo un error al crear el codigo de acceso del empleado");
            }
        }

        private bool UpdateAccessCodeProfile(Employee employee) {
            employeeManagerClient = new EmployeeManagerClient();
            try {
                string password = employeeManagerClient.UpdateAccessCodeProfile(employee);
                if (password != "Error") {
                    newAccessCode = password;
                    return true;
                } else {
                    return false;
                }
            } catch (Exception) {
                return false;
            }
        }

        private Employee CreateNewAccessCode() {
            SetEmployeeType();
            var updatedEmployee = new Employee {
                EmployeeId = employeeId
            };

            return updatedEmployee;
        }
    }
}
