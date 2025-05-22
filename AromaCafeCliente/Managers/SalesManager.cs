using AromaCafeCliente.AromaCafeService;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace AromaCafeCliente.Managers
{
    public class SalesManager
    {
        public static List<SalesData> GetSalesByDateRange(DateTime startDate, DateTime endDate)
        {
            List<SalesData> sales = new List<SalesData>();
            try
            {
                using (var proxy = new AromaCafeService.TableManagerClient())
                {
                    sales = new List<SalesData>(proxy.GetSalesReportByRange(startDate, endDate));
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
            return sales;
        }
        public static List<SaleByWaiterData> GetSalesByWWaiterAndDateRange(DateTime startDate, DateTime endDate)
        {
            List<SaleByWaiterData> sales = new List<SaleByWaiterData>();
            try
            {
                using (var proxy = new AromaCafeService.TableManagerClient())
                {
                    sales = new List<SaleByWaiterData>(proxy.GetSalesReportByWaiterRange(startDate, endDate));
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
            return sales;
        }
        public static List<FinancialMovement> GetFinancialReportByRange()
        {
            List<FinancialMovement> sales = new List<FinancialMovement>();
            DateTime endDate = DateTime.Today;
            DateTime startDate = endDate;
            try
            {
                using (var proxy = new AromaCafeService.TableManagerClient())
                {
                    sales = new List<FinancialMovement>(proxy.GetFinancialReportByRange(startDate, endDate));
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
            return sales;
        }
    }
}
