using AromaCafeCliente.AromaCafeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AromaCafeCliente.Managers
{
    public class ExpenseManager
    {
        public static List<Expense> GetExpensesByDay(DateTime date)
        {
            List<Expense> expenses = new List<Expense>();
            try
            {
                using (var proxy = new AromaCafeService.ExpenseManagerClient())
                {
                    expenses = new List<Expense>(proxy.GetAllExpensesByDay(date));
                }
            }
            catch (FaultException fe) { throw fe; }
            catch (CommunicationException ce) { throw ce; }
            catch (TimeoutException te) { throw te; }

            return expenses;
        }
    }
}
