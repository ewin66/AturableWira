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
using AturableWira.Module.BusinessObjects.ERP.Purchase;
using DevExpress.ExpressApp.Editors;

namespace AturableWira.Module.BusinessObjects.ERP.Inventory
{
   [DefaultClassOptions]
   //[ImageName("BO_Contact")]
   //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   public class InventoryReceiptItem : BaseObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public InventoryReceiptItem(Session session)
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
      InventoryReceipt inventoryReceipt;
      [Association("InventoryReceipt-Items")]
      public InventoryReceipt InventoryReceipt
      {
         get
         {
            return inventoryReceipt;
         }
         set
         {
            SetPropertyValue("InventoryReceipt", ref inventoryReceipt, value);
         }
      }

      OrderItem orderItem;
      public OrderItem OrderItem
      {
         get
         {
            return orderItem;
         }
         set
         {
            SetPropertyValue("OrderItem", ref orderItem, value);
         }
      }

      int quantityReceived;
      [ImmediatePostData]
      public int QuantityReceived
      {
         get
         {
            return quantityReceived;
         }
         set
         {
            SetPropertyValue("QuantityReceived", ref quantityReceived, value);
         }
      }
      int temporaryQuantity;
      [Browsable(false), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
      public int TemporaryQuantity
      {
         get
         {
            return temporaryQuantity;
         }
         set
         {
            SetPropertyValue("TemporaryQuantity", ref temporaryQuantity, value);
         }
      }

      [PersistentAlias("TemporaryQuantity - QuantityReceived")]
      public int QuantityOnOrder
      {
         get
         {
            int qty = (int)EvaluateAlias("QuantityOnOrder");
            if (qty < 0)
               return 0;
            else
               return qty;
         }
      }

      VendorItem vendorItem;
      public VendorItem VendorItem
      {
         get
         {
            return vendorItem;
         }
         set
         {
            SetPropertyValue("VendorItem", ref vendorItem, value);
         }
      }

      string lotNumber;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      public string LotNumber
      {
         get
         {
            return lotNumber;
         }
         set
         {
            SetPropertyValue("LotNumber", ref lotNumber, value);
         }
      }
      DateTime expiryDate;
      public DateTime ExpiryDate
      {
         get
         {
            return expiryDate;
         }
         set
         {
            SetPropertyValue("ExpiryDate", ref expiryDate, value);
         }
      }
      decimal unitCost;
      public decimal UnitCost
      {
         get
         {
            return unitCost;
         }
         set
         {
            SetPropertyValue("UnitCost", ref unitCost, value);
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