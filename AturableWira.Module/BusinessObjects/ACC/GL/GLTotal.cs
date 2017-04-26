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

namespace AturableWira.Module.BusinessObjects.ACC.GL
{
   [DefaultClassOptions]
   //[ImageName("BO_Contact")]
   //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   [ModelDefault("Caption", "GL Total")]
   [RuleCombinationOfPropertiesIsUnique("Account,PeriodYear")]
   public class GLTotal : BaseObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public GLTotal(Session session)
          : base(session)
      {
      }
      public override void AfterConstruction()
      {
         base.AfterConstruction();
         // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
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

      int periodYear;
      [ModelDefault("Caption", "Year")]
      [RuleRequiredField]
      [ModelDefault("DisplayFormat", "{0:d0}")]
      [ModelDefault("EditMask", "d0")]
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

      GLAccount account;
      [RuleRequiredField]
      public GLAccount Account
      {
         get
         {
            return account;
         }
         set
         {
            SetPropertyValue("Account", ref account, value);
         }
      }

      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal OpeningBalance
      {
         get
         {
            //sum = Session.Evaluate(GetType(OrderDetail), CriteriaOperator.Parse("Count()"), New BinaryOperator("Order", Oid))
            if (Account != null)
               return Convert.ToDecimal(Session.Evaluate<JournalEntry>(CriteriaOperator.Parse("Sum(Amount)"), CriteriaOperator.Parse("Voucher.PeriodYear = ? && Account.AccountNumber = ?", PeriodYear - 1, Account.AccountNumber)));
            else
               return 0;
         }
      }

      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      [PersistentAlias("Journals[Voucher.PeriodMonth=1].Sum(Amount)")]
      public decimal Period01
      {
         get
         {
            return Convert.ToDecimal(EvaluateAlias("Period01"));
         }
      }

      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      [PersistentAlias("Journals[Voucher.PeriodMonth<=2].Sum(Amount)")]
      public decimal Period02
      {
         get
         {
            return Convert.ToDecimal(EvaluateAlias("Period02"));
         }
      }

      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      [PersistentAlias("Journals[Voucher.PeriodMonth<=3].Sum(Amount)")]
      public decimal Period03
      {
         get
         {
            return Convert.ToDecimal(EvaluateAlias("Period03"));
         }
      }

      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      [PersistentAlias("Journals[Voucher.PeriodMonth<=4].Sum(Amount)")]
      public decimal Period04
      {
         get
         {
            return Convert.ToDecimal(EvaluateAlias("Period04"));
         }
      }

      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      [PersistentAlias("Journals[Voucher.PeriodMonth<=5].Sum(Amount)")]
      public decimal Period05
      {
         get
         {
            return Convert.ToDecimal(EvaluateAlias("Period05"));
         }
      }

      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      [PersistentAlias("Journals[Voucher.PeriodMonth<=6].Sum(Amount)")]
      public decimal Period06
      {
         get
         {
            return Convert.ToDecimal(EvaluateAlias("Period06"));
         }
      }

      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      [PersistentAlias("Journals[Voucher.PeriodMonth<=7].Sum(Amount)")]
      public decimal Period07
      {
         get
         {
            return Convert.ToDecimal(EvaluateAlias("Period07"));
         }
      }

      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      [PersistentAlias("Journals[Voucher.PeriodMonth<=8].Sum(Amount)")]
      public decimal Period08
      {
         get
         {
            return Convert.ToDecimal(EvaluateAlias("Period08"));
         }
      }

      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      [PersistentAlias("Journals[Voucher.PeriodMonth<=9].Sum(Amount)")]
      public decimal Period09
      {
         get
         {
            return Convert.ToDecimal(EvaluateAlias("Period09"));
         }
      }

      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      [PersistentAlias("Journals[Voucher.PeriodMonth<=10].Sum(Amount)")]
      public decimal Period10
      {
         get
         {
            return Convert.ToDecimal(EvaluateAlias("Period10"));
         }
      }

      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      [PersistentAlias("Journals[Voucher.PeriodMonth<=11].Sum(Amount)")]
      public decimal Period11
      {
         get
         {
            return Convert.ToDecimal(EvaluateAlias("Period11"));
         }
      }

      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      [PersistentAlias("Journals[Voucher.PeriodMonth<=12].Sum(Amount)")]
      public decimal Period12
      {
         get
         {
            return Convert.ToDecimal(EvaluateAlias("Period12"));
         }
      }


      XPCollection<JournalEntry> journals;
      public XPCollection<JournalEntry> Journals
      {
         get
         {
            return journals = new XPCollection<JournalEntry>(Session, CriteriaOperator.Parse("Voucher.PeriodYear=? and Account =? and Voucher.Posted = true", PeriodYear, Account));
         }
      }

   }
}
