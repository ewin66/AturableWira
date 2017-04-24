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
using AturableWira.Module.BusinessObjects.ERP;

namespace AturableWira.Module.BusinessObjects.ACC
{
   [DefaultClassOptions]
   //[ImageName("BO_Contact")]
   //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   public class Tax : XPLiteObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public Tax(Session session)
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
      string code;
      [Size(5)]
      [RuleUniqueValue,RuleRequiredField]
      [Key]
      public string Code
      {
         get
         {
            return code;
         }
         set
         {
            SetPropertyValue("Code", ref code, value);
         }
      }
      string description;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
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
      decimal rate;
      [ModelDefault("DisplayFormat","{0:N2}%")]
      [ModelDefault("EditMask", "N2")]
      public decimal Rate
      {
         get
         {
            return rate;
         }
         set
         {
            SetPropertyValue("Rate", ref rate, value);
         }
      }
      GLAccount gLAccount;
      [ModelDefault("Caption", "GL Account")]
      public GLAccount GLAccount
      {
         get
         {
            return gLAccount;
         }
         set
         {
            SetPropertyValue("GLAccount", ref gLAccount, value);
         }
      }

      [VisibleInDetailView(false)]
      [Association("Item-ApplicableTax")]
      public XPCollection<Item> Products
      {
         get
         {
            return GetCollection<Item>("Products");
         }
      }
   }
}