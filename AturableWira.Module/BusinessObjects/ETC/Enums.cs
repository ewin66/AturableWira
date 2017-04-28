using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AturableWira.Module.BusinessObjects.ETC
{
   public class Enums
   {
      public enum Gender
      {
         Undefined,
         Male,
         Female
      }
      public enum MaritalStatus
      {
         None,
         Single,
         Married,
         Divorced,
         Widowed
      }

      public enum CustomerStatus
      {
         Active = 1,
         Inactive = 0
      }

      public enum GLACcountType
      {
         Asset,
         Liability,
         Revenue,
         Expense,
         Equity
      }

      public enum CostingMethod
      {
         WeightedAvarage,
         Standard
      }

      public enum JournalVoucherSource
      {
         GLJV,
         ARInvoce,
         ARIventory,
         ARPayment,
         APInvoice,
         APInventory,
         APPayment
      }

      public enum Freight
      {
         Prepaid,
         PrepaidAndCharge,
         Pickup,
         Collect
      }

      public enum OrderStatus
      {
         WorkSheet,
         OnOrder,
         Completed,
         Canceled
      }
      public enum SalesOrderStatus
      {
         Quote,
         OnOrder,
         Completed,
         Canceled
      }

      public enum PaymentMethod
      {
         Cash,
         Cheque,
         BankTransfer,
         Visa,
         Mastercard,
         AmericanExpress,
         Others
      }
   }
}
