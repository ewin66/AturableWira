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
using AturableWira.Module.BusinessObjects.ERP.Sales;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace AturableWira.Module.BusinessObjects.ACC.AP
{
   [DefaultClassOptions]
   [NavigationItem(false)]
   //[ImageName("BO_Contact")]
   //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   [RuleCombinationOfPropertiesIsUnique("CollectionMustUnique", DefaultContexts.Save, "APPayment, Invoice")]
   [Appearance("APPaymentItemDisabled", TargetItems = "Amount", Enabled = false)]
   public class APPaymentItem : BaseObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public APPaymentItem(Session session)
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

      APPayment aPPayment;
      [Association("APPayment-Items")]
      [ModelDefault("Caption", "Payment")]
      public APPayment APPayment
      {
         get
         {
            return aPPayment;
         }
         set
         {
            SetPropertyValue("APPayment", ref aPPayment, value);
         }
      }
      APInvoice invoice;
      public APInvoice Invoice
      {
         get
         {
            return invoice;
         }
         set
         {
            SetPropertyValue("Invoice", ref invoice, value);
         }
      }
      //[PersistentAlias("Invoice.Owing")]
      decimal amount;
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal Amount
      {
         get
         {
            return amount;
         }
         set
         {
            SetPropertyValue("Amount", ref amount, value);
         }
      }
      [PersistentAlias("Amount-Payment")]
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal Owing
      {
         get
         {
            return Convert.ToDecimal(EvaluateAlias("Owing"));
         }
      }
      decimal payment;
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal Payment
      {
         get
         {
            return payment;
         }
         set
         {
            SetPropertyValue("Payment", ref payment, value);
         }
      }


   }
}