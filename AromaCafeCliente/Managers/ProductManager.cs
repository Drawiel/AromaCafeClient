using AromaCafeCliente.AromaCafeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AromaCafeCliente.Managers
{
    public class ProductManager
    {
        public static List<Product> GetProductsList()
        {
            List<Product> products = new List<Product>();
            try
            {
                using (var proxy = new AromaCafeService.ProductManagerClient())
                {
                    products = new List<Product> (proxy.GetAllProducts());
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
            return products;
        }
    }
}
