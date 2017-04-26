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
using static AturableWira.Module.BusinessObjects.ETC.Enums;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace AturableWira.Module.BusinessObjects.ACC.AP
{
   [DefaultClassOptions]
   [NavigationItem("Account Payable")]
   [ModelDefault("Caption", "Payment")]
   [Appearance("DisableDeleteWhenPosted", Criteria = "Posted", AppearanceItemType = "Action", TargetItems = "Delete", Enabled = false)]
   [Appearance("DisableEditWhenPosted", Criteria = "Posted", AppearanceItemType = "ViewItem", TargetItems = "*", Enabled = false)]
   //[ImageName("BO_Contact")]
   //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   public class APPayment : BaseObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public APPayment(Session session)
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
      Vendor vendor;
      [RuleRequiredField]
      [ImmediatePostData]
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

      PaymentMethod paymentMethod;
      public PaymentMethod PaymentMethod
      {
         get
         {
            return paymentMethod;
         }
         set
         {
            SetPropertyValue("PaymentMethod", ref paymentMethod, value);
         }
      }

      string reference;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      public string Reference
      {
         get
         {
            return reference;
         }
         set
         {
            SetPropertyValue("Reference", ref reference, value);
         }
      }

      Bank bank;
      [RuleRequiredField]
      public Bank Bank
      {
         get
         {
            return bank;
         }
         set
         {
            SetPropertyValue("Bank", ref bank, value);
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
      [ModelDefault("Caption", "Month")]
      [ModelDefault("DisplayFormat", "{0:d0}")]
      [RuleRange(1, 12)]
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
      [RuleRequiredField]
      [ModelDefault("Caption", "Year")]
      [ModelDefault("DisplayFormat", "{0:d0}")]
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

      bool isVoid;
      [ModelDefault("Caption", "Void")]
      public bool IsVoid
      {
         get
         {
            return isVoid;
         }
         set
         {
            SetPropertyValue("IsVoid", ref isVoid, value);
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
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      [PersistentAlias("Items.Sum(Payment)")]
      public decimal Amount
      {
         get
         {
            return Convert.ToDecimal(EvaluateAlias("Amount"));
         }
      }

      [Association("APPayment-Items"), Aggregated]
      public XPCollection<APPaymentItem> Items
      {
         get
         {
            return GetCollection<APPaymentItem>("Items");
         }
      }
   }
}