using AromaCafeCliente.AromaCafeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AromaCafeCliente.Managers
{
    public class EmployeeManager
    {
        public static Employee ValidateCredentials(string username, string password)
        {
            Employee employee = new Employee();
            try
            {
                using (var proxy = new AromaCafeService.LogInManagerClient())
                {
                    employee = proxy.ValidateCredentials(username, password);
                }
            }
            catch (FaultException faultException)
            {
                throw faultException;
            }
            catch (CommunicationException communicationException)
            {
                throw communicationException;
            }
            catch (TimeoutException timeoutException)
            {
                throw timeoutException;
            }
            return employee;
        }

        public static int LogOut(string password)
        {
            int loggedOut = 0;
            try
            {
                using (var proxy = new AromaCafeService.LogInManagerClient())
                {
                    loggedOut = proxy.CloseTurn(password);
                }
            }
            catch (FaultException faultException)
            {
                throw faultException;
            }
            catch (CommunicationException communicationException)
            {
                throw communicationException;
            }
            catch (TimeoutException timeoutException)
            {
                throw timeoutException;
            }
            return loggedOut;
        }

        public static string RegisterEmployee(Employee employee)
        {
            string registered;
            try
            {
                using (var proxy = new AromaCafeService.EmployeeManagerClient())
                {
                    registered = proxy.RegisterEmployee(employee);
                }
            }
            catch (FaultException faultException)
            {
                throw faultException;
            }
            catch (CommunicationException communicationException)
            {
                throw communicationException;
            }
            catch (TimeoutException timeoutException)
            {
                throw timeoutException;
            }
            return registered;
        }
    }
}
