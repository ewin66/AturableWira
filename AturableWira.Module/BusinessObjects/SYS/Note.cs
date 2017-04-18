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
using DevExpress.ExpressApp.Editors;
using AturableWira.Module.BusinessObjects.HRM;
using DevExpress.ExpressApp.SystemModule;

namespace AturableWira.Module.BusinessObjects.SYS
{
   [DefaultClassOptions]
   [ImageName("BO_Note")]
   [DefaultProperty("Title")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   [ListViewFilter("NoteFilterCriteriaAll", "[IsPrivate] = False or ([IsPrivate] = True and [Owner.Oid] = CurrentUserId())", "All Notes", true, Index = 0)]
   [ListViewFilter("NoteFilterCriteriaOnlyMyNotes", "[Owner.Oid] = CurrentUserId()", "Only My Notes", Index = 1)]
   public class Note : BaseObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public Note(Session session)
          : base(session)
      {
      }
      public override void AfterConstruction()
      {
         base.AfterConstruction();
         // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
         Owner = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
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
      string title;
      [Size(SizeAttribute.Unlimited)]
      [EditorAlias(EditorAliases.StringPropertyEditor)]
      [ModelDefault("RowCount","1")]
      [RuleRequiredField]
      public string Title
      {
         get
         {
            return title;
         }
         set
         {
            SetPropertyValue("Title", ref title, value);
         }
      }
      FileData attachment;
      public FileData Attachment
      {
         get
         {
            return attachment;
         }
         set
         {
            SetPropertyValue("Attachment", ref attachment, value);
         }
      }
      Employee owner;
      public Employee Owner
      {
         get
         {
            return owner;
         }
         set
         {
            SetPropertyValue("Owner", ref owner, value);
         }
      }
      bool isPrivate;
      public bool IsPrivate
      {
         get
         {
            return isPrivate;
         }
         set
         {
            SetPropertyValue("IsPrivate", ref isPrivate, value);
         }
      }
      string description;
      [Size(SizeAttribute.Unlimited)]
      [EditorAlias(EditorAliases.HtmlPropertyEditor)]
      [RuleRequiredField]
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
   }
}