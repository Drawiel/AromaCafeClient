using AromaCafeCliente.AromaCafeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace AromaCafeCliente.Managers
{
    public class OrderManager
    {
        public static List<ProductOrder> GetOrdersByTable(int idTable)
        {
            var orders = new List<ProductOrder>();
            try
            {
                using(var proxy = new AromaCafeService.OrderManagerClient())
                {
                    orders = proxy.GetOrdersByTable(idTable).ToList();
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
            return orders;
        }

        public static int MarkOrderAsDelivered(string productOrderName, int tableId)
        {
            int marked = 0;
            try
            {
                using (var proxy = new AromaCafeService.OrderManagerClient())
                {
                    marked = proxy.MarkOrderAsDelivered(tableId, productOrderName);
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
            return marked;

        }

        internal static int EditOrderQuantity(int tableId, string productName, int quantity)
        {
            int marked = 0;
            try
            {
                using (var proxy = new AromaCafeService.OrderManagerClient())
                {
                    marked = proxy.EditOrderQuantity(tableId, productName, quantity);
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
            return marked;
        }
    

        internal static int MarkOrderAsRequested(string productOrderName, int tableId)
        {
            int marked = 0;
            try
            {
                using (var proxy = new AromaCafeService.OrderManagerClient())
                {
                    marked = proxy.MarkOrderAsRequested(tableId, productOrderName);
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
            return marked;
        }
    }
}
