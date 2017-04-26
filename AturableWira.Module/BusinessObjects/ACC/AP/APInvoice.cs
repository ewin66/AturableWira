using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using AturableWira.Module.BusinessObjects.CRM;
using AturableWira.Module.BusinessObjects.ERP.Purchase;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace AturableWira.Module.BusinessObjects.ACC.AP
{
   [DefaultClassOptions]
   //[ImageName("BO_Contact")]
   [DefaultProperty("InvoiceNumber")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   [ModelDefault("Caption", "Invoice")]
   [NavigationItem("Account Payable")]
   [Appearance("PostedInvoiceDisableAll", Criteria = "Posted = True", Enabled = false, TargetItems = "*")]
   [Appearance("DisableDeleteWhenPosted", Criteria = "Posted", AppearanceItemType = "Action", TargetItems = "Delete", Enabled = false)]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   public class APInvoice : XPLiteObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public APInvoice(Session session)
          : base(session)
      {
      }
      public override void AfterConstruction()
      {
         base.AfterConstruction();
         // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
         InvoiceDate = DateTime.Now;
         PeriodMonth = DateTime.Now.Month;
         PeriodYear = DateTime.Now.Year;
      }
      //private string _PersistentProperty;
      //[XafDisplayName("My display name"), ToolTip("My hint message")]
      //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
      //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
      //public string PersistentProperty {
      //    get { return _PersistentProperty; }
      //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
      //}

      //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
      //public void ActionMethod() {
      //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
      //    this.PersistentProperty = "Paid";
      //}

      Vendor vendor;
      public Vendor Vendor
      {
         get
         {
            return vendor;
         }
         set
         {
            if (SetPropertyValue("Vendor", ref vendor, value))
            {
               if (!IsLoading)
               {
                  if (Vendor.DefaultDueDays > 0)
                     DueDate = InvoiceDate.AddDays(Vendor.DefaultDueDays);

                  if (Vendor.DefaultDescription != null)
                     Description = Vendor.DefaultDescription;
                  else
                     Description = null;
               }
            }
         }
      }

      string invoiceNumber;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      [RuleRequiredField, RuleUniqueValue, Key]
      [VisibleInListView(true)]
      [ModelDefault("Caption", "Inv No.")]
      public string InvoiceNumber
      {
         get
         {
            return invoiceNumber;
         }
         set
         {
            SetPropertyValue("InvoiceNumber", ref invoiceNumber, value);
         }

      }

      PurchaseOrder purchaseOrder;
      [ModelDefault("Caption", "PO No.")]
      public PurchaseOrder PurchaseOrder
      {
         get
         {
            return purchaseOrder;
         }
         set
         {
            SetPropertyValue("PurchaseOrder", ref purchaseOrder, value);
         }
      }

      string description;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      [RuleRequiredField]
      public string Description
      {
         get
         {
            return description;
         }
         set
         {
            SetPropertyValue("Description", ref description, value);
         }
      }

      [PersistentAlias("Items.Sum(Amount)")]
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal Amount
      {
         get
         {
            if (Items.Count > 0)
               return (decimal)EvaluateAlias("Amount");
            else return 0;
         }
      }

      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal Paid
      {
         get
         {
            return Convert.ToDecimal(Session.Evaluate<APPaymentItem>(CriteriaOperator.Parse("Sum(Payment)"), CriteriaOperator.Parse("Invoice.InvoiceNumber = ? and APPayment.Posted = true", InvoiceNumber)));
         }
      }

      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      [PersistentAlias("Amount - Paid")]
      public decimal Owing
      {
         get
         {
            return (Decimal)EvaluateAlias("Owing");
         }
      }

      DateTime invoiceDate;
      public DateTime InvoiceDate
      {
         get
         {
            return invoiceDate;
         }
         set
         {
            if (SetPropertyValue("InvoiceDate", ref invoiceDate, value))
            {
               if (!IsLoading)
                  if (Vendor != null)
                  {
                     if (Vendor.DefaultDueDays > 0)
                     {
                        DueDate = InvoiceDate.AddDays(Vendor.DefaultDueDays);
                     }
                  }
            }
         }
      }

      DateTime dueDate;
      [RuleRequiredField]
      public DateTime DueDate
      {
         get
         {
            return dueDate;
         }
         set
         {
            SetPropertyValue("DueDate", ref dueDate, value);
         }
      }

      public DateTime DiscountDate
      {
         get
         {
            if (Vendor != null)
            {
               if (Vendor.DiscountDays > 0)
                  return InvoiceDate.AddDays(Vendor.DiscountDays);
               else
                  return DateTime.MinValue;
            }
            else
               return DateTime.MinValue;
         }
      }

      int periodMonth;
      [RuleRange(1, 12)]
      [ModelDefault("Caption", "Month")]
      public int PeriodMonth
      {
         get
         {
            return periodMonth;
         }
         set
         {
            SetPropertyValue("PeriodMonth", ref periodMonth, value);
         }
      }

      int periodYear;
      [ModelDefault("DisplayFormat", "{0:d0}")]
      [ModelDefault("EditMask", "d0")]
      [ModelDefault("Caption", "Year")]
      public int PeriodYear
      {
         get
         {
            return periodYear;
         }
         set
         {
            SetPropertyValue("PeriodYear", ref periodYear, value);
         }
      }

      bool posted;
      [CaptionsForBoolValues("Posted", "Not Posted")]
      public bool Posted
      {
         get
         {
            return posted;
         }
         set
         {
            SetPropertyValue("Posted", ref posted, value);
         }
      }

      

      [Association("APInvoice-Items"), Aggregated]
      public XPCollection<APInvoiceItem> Items
      {
         get
         {
            return GetCollection<APInvoiceItem>("Items");
         }
      }
   }
}