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
using AturableWira.Module.BusinessObjects.ERP;
using DevExpress.ExpressApp.Editors;

namespace AturableWira.Module.BusinessObjects.ACC.AR
{
   [DefaultClassOptions]
   [CreatableItem(false)]
   [NavigationItem(false)]
   //[ImageName("BO_Contact")]
   //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   public class ARInvoiceItem : BaseObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public ARInvoiceItem(Session session)
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

      ARInvoice aRInvoice;
      [ModelDefault("Caption", "Invoice")]
      [Association("ARInvoice-Items")]
      public ARInvoice ARInvoice
      {
         get
         {
            return aRInvoice;
         }
         set
         {
            SetPropertyValue("ARInvoice", ref aRInvoice, value);
         }
      }
      int quantity;
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

      Item item;
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

      decimal unitPrice;
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

      [PersistentAlias("Quantity* UnitPrice")]
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