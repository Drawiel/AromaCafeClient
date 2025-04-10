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
        public static Product GetProduct(int productId)
        {
            Product product = null;
            try
            {
                using (var proxy = new AromaCafeService.ProductManagerClient())
                {
                    product = proxy.GetProduct(productId);
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
            return product;
        }
        public static int UpdateProduct(Product product)
        {
            int result;
            try
            {
                using (var proxy = new AromaCafeService.ProductManagerClient())
                {
                    result = proxy.UpdateProduct(product);
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

        public static int AddProduct(Product newProduct)
        {
            int result;
            try
            {
                using (var proxy = new AromaCafeService.ProductManagerClient())
                {
                    result = proxy.AddProduct(newProduct);
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
        public static int UpdateProductStock(int productId, int newStock)
        {
            int result;
            try
            {
                using (var proxy = new AromaCafeService.ProductManagerClient())
                {
                    result = proxy.IncreaseStock(productId, newStock);
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
