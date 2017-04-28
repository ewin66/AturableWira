using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Editors;

namespace AturableWira.Module.BusinessObjects.ERP.Sales
{
   [DefaultClassOptions]
   [CreatableItem(false)]
   [NavigationItem(false)]
   //[ImageName("BO_Contact")]
   //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   public class SalesOrderItem : BaseObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public SalesOrderItem(Session session)
          : base(session)
      {
      }
      public override void AfterConstruction()
      {
         base.AfterConstruction();
         // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
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

      SalesOrder salesOrder;
      [Association("SalesOrder-Items")]
      public SalesOrder SalesOrder
      {
         get
         {
            return salesOrder;
         }
         set
         {
            SetPropertyValue("SalesOrder", ref salesOrder, value);
         }
      }

      Item item;
      [RuleRequiredField]
      public Item Item
      {
         get
         {
            return item;
         }
         set
         {
            SetPropertyValue("Item", ref item, value);
         }
      }

      int quantity;
      [RuleValueComparison(ValueComparisonType.GreaterThanOrEqual, 1)]
      public int Quantity
      {
         get
         {
            return quantity;
         }
         set
         {
            SetPropertyValue("Quantity", ref quantity, value);
         }
      }

      decimal unitPrice;
      [ModelDefault("EditMask", "n2")]
      [ModelDefault("DisplayFormat", "{0:n2}")]
      public decimal UnitPrice
      {
         get
         {
            return unitPrice;
         }
         set
         {
            SetPropertyValue("UnitPrice", ref unitPrice, value);
         }
      }

      [ModelDefault("EditMask", "n2")]
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [PersistentAlias("Quantity * UnitPrice")]
      public decimal Amount
      {
         get
         {
            return Convert.ToDecimal(EvaluateAlias("Amount"));
         }
      }

      string notes;
      [Size(SizeAttribute.Unlimited)]
      [ModelDefault("RowCount", "1")]
      [EditorAlias(EditorAliases.StringPropertyEditor)]
      public string Notes
      {
         get
         {
            return notes;
         }
         set
         {
            SetPropertyValue("Notes", ref notes, value);
         }
      }
   }
}