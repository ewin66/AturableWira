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
using AturableWira.Module.BusinessObjects.CRM;
using DevExpress.ExpressApp.Editors;
using AturableWira.Module.BusinessObjects.ACC;

namespace AturableWira.Module.BusinessObjects.ERP.Purchase
{
   [DefaultClassOptions]
   [NavigationItem("Purchasing")]
   //[ImageName("BO_Contact")]
   [DefaultProperty("DisplayName")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   public class VendorItem : BaseObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public VendorItem(Session session)
          : base(session)
      {
      }
      public override void AfterConstruction()
      {
         base.AfterConstruction();
         // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
         VendorItemQuantity = 1;
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
      protected override void OnSaving()
      {
         base.OnSaving();
      }

      private const string displayFormat = "Vendor: {Vendor} Item: {VendorItemNumber} - {Item}";
      [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
      public string DisplayName
      {
         get
         {
            return ObjectFormatter.Format(displayFormat, this, EmptyEntriesMode.RemoveDelimiterWhenEntryIsEmpty);
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

      Vendor vendor;
      [Association("Vendor-Items")]
      [RuleRequiredField]
      public Vendor Vendor
      {
         get
         {
            return vendor;
         }
         set
         {
            SetPropertyValue("Vendor", ref vendor, value);
         }
      }

      string vendorItemNumber;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      public string VendorItemNumber
      {
         get
         {
            return vendorItemNumber;
         }
         set
         {
            SetPropertyValue("VendorItemNumber", ref vendorItemNumber, value);
         }
      }

      bool suspended;
      [ModelDefault("ToolTip", "Suspended vendor will not be selectable on new transactions")]
      public bool Suspended
      {
         get
         {
            return suspended;
         }
         set
         {
            SetPropertyValue("Suspended", ref suspended, value);
         }
      }

      string description;
      [Size(SizeAttribute.Unlimited)]
      [ModelDefault("RowCount", "1")]
      [EditorAlias(EditorAliases.StringPropertyEditor)]
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

      int vendorItemQuantity;
      [ModelDefault("Caption", "Quantity per V.Item")]
      [RuleValueComparison(ValueComparisonType.GreaterThanOrEqual, 1)]
      [ImmediatePostData]
      public int VendorItemQuantity
      {
         get
         {
            return vendorItemQuantity;
         }
         set
         {
            if (SetPropertyValue("VendorItemQuantity", ref vendorItemQuantity, value))
               if (!IsLoading)
                  if (VendorItemQuantity != 0)
                     if (Vendor != null)
                        OurItemCost = (cost * Vendor.Currency.ExchangeRate) / VendorItemQuantity;
         }
      }

      decimal cost;
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      [ModelDefault("ToolTip", "Cost in vendor's currency")]
      public decimal Cost
      {
         get
         {
            return cost;
         }
         set
         {
            if (SetPropertyValue("Cost", ref cost, value))
               if (!IsLoading)
                  if (VendorItemQuantity != 0)
                     if (Vendor != null)
                        OurItemCost = (cost * Vendor.Currency.ExchangeRate) / VendorItemQuantity;
         }
      }
      DateTime costDate;
      public DateTime CostDate
      {
         get
         {
            return costDate;
         }
         set
         {
            SetPropertyValue("CostDate", ref costDate, value);
         }
      }
      decimal ourItemCost;
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      [ModelDefault("ToolTip", "Our item cost is based on cost, number of vendor's item and exchange rate")]
      public decimal OurItemCost
      {
         get
         {
            return ourItemCost;
         }
         set
         {
            SetPropertyValue("OurItemCost", ref ourItemCost, value);
         }
      }
   }
}