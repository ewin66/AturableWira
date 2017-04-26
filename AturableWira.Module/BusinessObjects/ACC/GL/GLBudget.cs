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
   [ModelDefault("Caption", "GL Budget")]
   //[ImageName("BO_Contact")]
   //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   [RuleCombinationOfPropertiesIsUnique("Account,PeriodYear")]
   public class GLBudget : BaseObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public GLBudget(Session session)
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
      GLAccount account;
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

      int periodYear;
      [ModelDefault("Caption", "Year")]
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

      decimal budget01;
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal Budget01
      {
         get
         {
            return budget01;
         }
         set
         {
            SetPropertyValue("Budget01", ref budget01, value);
         }
      }
      decimal budget02;
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal Budget02
      {
         get
         {
            return budget02;
         }
         set
         {
            SetPropertyValue("Budget02", ref budget02, value);
         }
      }
      decimal budget03;
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal Budget03
      {
         get
         {
            return budget03;
         }
         set
         {
            SetPropertyValue("Budget03", ref budget03, value);
         }
      }
      decimal budget04;
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal Budget04
      {
         get
         {
            return budget04;
         }
         set
         {
            SetPropertyValue("Budget04", ref budget04, value);
         }
      }
      decimal budget05;
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal Budget05
      {
         get
         {
            return budget05;
         }
         set
         {
            SetPropertyValue("Budget05", ref budget05, value);
         }
      }
      decimal budget06;
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal Budget06
      {
         get
         {
            return budget06;
         }
         set
         {
            SetPropertyValue("Budget06", ref budget06, value);
         }
      }
      decimal budget07;
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal Budget07
      {
         get
         {
            return budget07;
         }
         set
         {
            SetPropertyValue("Budget07", ref budget07, value);
         }
      }
      decimal budget08;
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal Budget08
      {
         get
         {
            return budget08;
         }
         set
         {
            SetPropertyValue("Budget08", ref budget08, value);
         }
      }
      decimal budget09;
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal Budget09
      {
         get
         {
            return budget09;
         }
         set
         {
            SetPropertyValue("Budget09", ref budget09, value);
         }
      }
      decimal budget10;
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal Budget10
      {
         get
         {
            return budget10;
         }
         set
         {
            SetPropertyValue("Budget10", ref budget10, value);
         }
      }
      decimal budget11;
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal Budget11
      {
         get
         {
            return budget11;
         }
         set
         {
            SetPropertyValue("Budget11", ref budget11, value);
         }
      }
      decimal budget12;
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal Budget12
      {
         get
         {
            return budget12;
         }
         set
         {
            SetPropertyValue("Budget12", ref budget12, value);
         }
      }
   }
}