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
using AturableWira.Module.BusinessObjects.ACC.GL;

namespace AturableWira.Module.BusinessObjects.ACC
{
   [DefaultClassOptions]
   //[ImageName("BO_Contact")]
   //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   public class Currency : XPLiteObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public Currency(Session session)
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
      string currencyCode;
      [Size(3)]
      [RuleRequiredField]
      [RuleUniqueValue]
      [Key]
      public string CurrencyCode
      {
         get
         {
            return currencyCode;
         }
         set
         {
            SetPropertyValue("CurrencyCode", ref currencyCode, value);
         }
      }

      string name;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      [RuleRequiredField]
      public string Name
      {
         get
         {
            return name;
         }
         set
         {
            SetPropertyValue("Name", ref name, value);
         }
      }
      decimal exchangeRate;
      public decimal ExchangeRate
      {
         get
         {
            return exchangeRate;
         }
         set
         {
            SetPropertyValue("ExchangeRate", ref exchangeRate, value);
         }
      }

      GLAccount accountsPayable;
      public GLAccount AccountsPayable
      {
         get
         {
            return accountsPayable;
         }
         set
         {
            SetPropertyValue("AccountsPayable", ref accountsPayable, value);
         }
      }

      GLAccount aPDiscounts;
      [ModelDefault("Caption", "AP Discounts")]
      public GLAccount APDiscounts
      {
         get
         {
            return aPDiscounts;
         }
         set
         {
            SetPropertyValue("APDiscounts", ref aPDiscounts, value);
         }
      }

      GLAccount accountsReceivable;
      public GLAccount AccountsReceivable
      {
         get
         {
            return accountsReceivable;
         }
         set
         {
            SetPropertyValue("AccountsReceivable", ref accountsReceivable, value);
         }
      }
      GLAccount aRDiscounts;
      [ModelDefault("Caption", "AR Discounts")]
      public GLAccount ARDiscounts
      {
         get
         {
            return aRDiscounts;
         }
         set
         {
            SetPropertyValue("ARDiscounts", ref aRDiscounts, value);
         }
      }

      GLAccount defaultARWriteOffs;
      [ModelDefault("Caption", "Default AR Write-offs")]
      public GLAccount DefaultARWriteOffs
      {
         get
         {
            return defaultARWriteOffs;
         }
         set
         {
            SetPropertyValue("DefaultARWriteOffs", ref defaultARWriteOffs, value);
         }
      }

      GLAccount gainLossOnExchange;
      [ModelDefault("Caption", "Gain/Loss on Exchange")]
      public GLAccount GainLossOnExchange
      {
         get
         {
            return gainLossOnExchange;
         }
         set
         {
            SetPropertyValue("GainLossOnExchange", ref gainLossOnExchange, value);
         }
      }
   }
}