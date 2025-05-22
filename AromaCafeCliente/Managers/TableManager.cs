using AromaCafeCliente.AromaCafeService;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace AromaCafeCliente.Managers
{
    public class TableManager
    {
        public static List<TableCustomer> GetActiveAndClosedTables()
        {
            List<TableCustomer> tables = new List<TableCustomer>();
            try
            {
                using (var proxy = new AromaCafeService.TableManagerClient())
                {
                    tables = new List<TableCustomer>(proxy.GetActiveAndClosedTables());
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
            return tables;
        }

        public static int CreateNewTable(TableCustomer table)
        {
            int result;
            try
            {
                using (var proxy = new AromaCafeService.TableManagerClient())
                {
                    result = proxy.NewTable(table);
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
            return result;
        }

        public static int ChargeTableBill(Charge charge)
        {
            int result;
            try
            {
                using (var proxy = new AromaCafeService.TableManagerClient())
                {
                    result = proxy.ChargeBill(charge);
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
            return result;
        }

        public static int CloseTable(int tableId)
        {
            int result;
            try
            {
                using (var proxy = new AromaCafeService.TableManagerClient())
                {
                    result = proxy.CloseTable(tableId);
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
            return result;
        }
    }
}