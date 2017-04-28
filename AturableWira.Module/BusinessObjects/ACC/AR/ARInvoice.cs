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
using DevExpress.ExpressApp.Editors;

namespace AturableWira.Module.BusinessObjects.ACC.AR
{
   [DefaultClassOptions]
   [ModelDefault("Caption", "Invoice")]
   [NavigationItem("Account Receivable")]
   //[ImageName("BO_Contact")]
   //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   public class ARInvoice : BaseObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public ARInvoice(Session session)
          : base(session)
      {
      }
      public override void AfterConstruction()
      {
         base.AfterConstruction();
         // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
         Date = DateTime.Now;
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

      string invoiceNumber;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      [RuleRequiredField]
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
   
      DateTime date;
      [RuleRequiredField]
      public DateTime Date
      {
         get
         {
            return date;
         }
         set
         {
            SetPropertyValue("Date", ref date, value);
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
      [RuleRequiredField]
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

      Customer customer;
      [RuleRequiredField]
      public Customer Customer
      {
         get
         {
            return customer;
         }
         set
         {
            SetPropertyValue("Customer", ref customer, value);
         }
      }

      decimal amount = 0;
      [ModelDefault("EditMask", "n2")]
      [ModelDefault("DisplayFormat", "{0:n2}")]
      public decimal Amount
      {
         get
         {
            return amount;
         }
      }

      decimal paid = 0;
      [ModelDefault("EditMask", "n2")]
      [ModelDefault("DisplayFormat", "{0:n2}")]
      public decimal Paid
      {
         get
         {
            return paid;
         }
      }

      decimal owing = 0;
      [ModelDefault("EditMask", "n2")]
      [ModelDefault("DisplayFormat", "{0:n2}")]
      public decimal Owing
      {
         get
         {
            return owing;
         }
      }

      string description;
      [Size(SizeAttribute.Unlimited)]
      [ModelDefault("RowCount", "1")]
      [EditorAlias(EditorAliases.StringPropertyEditor)]
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

      [Association("ARInvoice-Items"), Aggregated]
      public XPCollection<ARInvoiceItem> Items
      {
         get
         {
            return GetCollection<ARInvoiceItem>("Items");
         }
      }
   }
}