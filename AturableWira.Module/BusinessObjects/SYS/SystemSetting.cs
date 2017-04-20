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
using AturableWira.Module.BusinessObjects.ACC.GL;

namespace AturableWira.Module.BusinessObjects.SYS
{
   [DefaultClassOptions]
   [NavigationItem(false)]
   //[ImageName("BO_Contact")]
   //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   [RuleObjectExists("AnotherSettingExists", DefaultContexts.Save, "True", InvertResult = true, CustomMessageTemplate = "Another setting object already exists.")]
   [RuleCriteria("CannotDeleteSetting", DefaultContexts.Delete, "False", CustomMessageTemplate = "Cannot delete Setting.")]
   public class SystemSetting : BaseObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public SystemSetting(Session session)
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
      string companyName;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      [RuleRequiredField]
      public string CompanyName
      {
         get
         {
            return companyName;
         }
         set
         {
            SetPropertyValue("CompanyName", ref companyName, value);
         }
      }
      AddressDetail address;
      [Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
      public AddressDetail Address
      {
         get
         {
            return address;
         }
         set
         {
            SetPropertyValue("Address", ref address, value);
         }
      }
      decimal creditLimit;
      public decimal CreditLimit
      {
         get
         {
            return creditLimit;
         }
         set
         {
            SetPropertyValue("CreditLimit", ref creditLimit, value);
         }
      }
      MediaDataObject companyLogo;
      public MediaDataObject CompanyLogo
      {
         get
         {
            return companyLogo;
         }
         set
         {
            SetPropertyValue("CompanyLogo", ref companyLogo, value);
         }
      }
      GLAccount sales;
      [RuleRequiredField]
      public GLAccount Sales
      {
         get
         {
            return sales;
         }
         set
         {
            SetPropertyValue("Sales", ref sales, value);
         }
      }
      GLAccount costofGoodsSold;
      [ModelDefault("Caption", "Cost of Goods Sold")]
      [RuleRequiredField]
      public GLAccount CostofGoodsSold
      {
         get
         {
            return costofGoodsSold;
         }
         set
         {
            SetPropertyValue("CostofGoodsSold", ref costofGoodsSold, value);
         }
      }

      GLAccount inventory;
      [RuleRequiredField]
      public GLAccount Inventory
      {
         get
         {
            return inventory;
         }
         set
         {
            SetPropertyValue("Inventory", ref inventory, value);
         }
      }
      AturableWira.Module.BusinessObjects.ACC.Currency defaultCurrency;
      [RuleRequiredField]
      public AturableWira.Module.BusinessObjects.ACC.Currency DefaultCurrency
      {
         get
         {
            return defaultCurrency;
         }
         set
         {
            SetPropertyValue("DefaultCurrency", ref defaultCurrency, value);
         }
      }

      int invoiceNumber;
      [ModelDefault("Caption", "Next Invoice Number")]
      [ModelDefault("EditMask","d0")]
      [ModelDefault("DisplayFormat", "{0:d0}")]
      public int InvoiceNumber
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
      int salesNumber;
      [ModelDefault("Caption", "Next Sales Order Number")]
      [ModelDefault("EditMask", "d0")]
      [ModelDefault("DisplayFormat", "{0:d0}")]
      public int SalesNumber
      {
         get
         {
            return salesNumber;
         }
         set
         {
            SetPropertyValue("SalesNumber", ref salesNumber, value);
         }
      }
      int purchaseOrderNumber;
      [ModelDefault("Caption", "Next Purchase Order Number")]
      [ModelDefault("EditMask", "d0")]
      [ModelDefault("DisplayFormat", "{0:d0}")]
      public int PurchaseOrderNumber
      {
         get
         {
            return purchaseOrderNumber;
         }
         set
         {
            SetPropertyValue("PurchaseOrderNumber", ref purchaseOrderNumber, value);
         }
      }
      int inventoryReceiptNumber;
      [ModelDefault("Caption", "Next Inventory Receipt Number")]
      [ModelDefault("EditMask", "d0")]
      [ModelDefault("DisplayFormat", "{0:d0}")]
      public int InventoryReceiptNumber
      {
         get
         {
            return inventoryReceiptNumber;
         }
         set
         {
            SetPropertyValue("InventoryReceiptNumber", ref inventoryReceiptNumber, value);
         }
      }
   }
}