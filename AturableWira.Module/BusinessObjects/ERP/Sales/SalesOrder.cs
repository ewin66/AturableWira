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
using DevExpress.ExpressApp.Editors;
using AturableWira.Module.BusinessObjects.CRM;
using static AturableWira.Module.BusinessObjects.ETC.Enums;

namespace AturableWira.Module.BusinessObjects.ERP.Sales
{
   [DefaultClassOptions]
   //[ImageName("BO_Contact")]
   [DefaultProperty("OrderNumber")]
   [NavigationItem("Sales")]
   [ModelDefault("Caption", "Sales Order")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   public class SalesOrder : XPLiteObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public SalesOrder(Session session)
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
      string orderNumber;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      [RuleRequiredField]
      [VisibleInListView(true), VisibleInDetailView(true), Key]
      public string OrderNumber
      {
         get
         {
            return orderNumber;
         }
         set
         {
            SetPropertyValue("OrderNumber", ref orderNumber, value);
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

      DateTime requestShipDate;
      [RuleRequiredField]
      public DateTime RequestShipDate
      {
         get
         {
            return requestShipDate;
         }
         set
         {
            SetPropertyValue("RequestShipDate", ref requestShipDate, value);
         }
      }

      Freight  freight;
      public Freight Freight
      {
         get
         {
            return freight;
         }
         set
         {
            SetPropertyValue("Freight", ref freight, value);
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

      [Association("SalesOrder-Items"), Aggregated]
      public XPCollection<SalesOrderItem> Items
      {
         get
         {
            return GetCollection<SalesOrderItem>("Items");
         }
      }
   }
}