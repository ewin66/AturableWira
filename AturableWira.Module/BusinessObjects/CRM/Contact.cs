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
using AturableWira.Module.BusinessObjects.SYS;
using AturableWira.Module.BusinessObjects.HRM;
using static AturableWira.Module.BusinessObjects.ETC.Enums;

namespace AturableWira.Module.BusinessObjects.CRM
{
  [DefaultClassOptions]
  [ImageName("BO_Contact")]
  //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
  //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
  //[Persistent("DatabaseTableName")]
  // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
  public class Contact : Account
  { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    public Contact(Session session)
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

    string firstName;
    [Size(SizeAttribute.DefaultStringMappingFieldSize)]
    [VisibleInListView(false)]
    [VisibleInLookupListView(false)]
    [ImmediatePostData(true)]
    public string FirstName
    {
      get
      {
        return firstName;
      }
      set
      {
        if (SetPropertyValue("FirstName", ref firstName, value))
        {
          if (!IsLoading)
          {
            Name = FullName;
          }
        }
      }
    }
    string lastName;
    [Size(SizeAttribute.DefaultStringMappingFieldSize)]
    [VisibleInListView(false)]
    [VisibleInLookupListView(false)]
    [ImmediatePostData(true)]
    public string LastName
    {
      get
      {
        return lastName;
      }
      set
      {
        if (SetPropertyValue("LastName", ref lastName, value))
        {
          {
            if (!IsLoading)
            {
              Name = FullName;
            }
          }
        }
      }
    }
    [PersistentAlias("Concat([FirstName], ' ', [LastName])")]
    [Browsable(false), VisibleInListView(false), VisibleInDetailView(false)]
    public string FullName
    {
      get
      {
        return (string)EvaluateAlias("FullName");
      }
    }
    MediaDataObject photo;
    public MediaDataObject Photo
    {
      get
      {
        return photo;
      }
      set
      {
        SetPropertyValue("Photo", ref photo, value);
      }
    }
    string jobTitle;
    [Size(SizeAttribute.DefaultStringMappingFieldSize)]
    public string JobTitle
    {
      get
      {
        return jobTitle;
      }
      set
      {
        SetPropertyValue("JobTitle", ref jobTitle, value);
      }
    }
    DateTime birthday;
    public DateTime Birthday
    {
      get
      {
        return birthday;
      }
      set
      {
        SetPropertyValue("Birthday", ref birthday, value);
      }
    }
    DateTime anniversary;
    public DateTime Anniversary
    {
      get
      {
        return anniversary;
      }
      set
      {
        SetPropertyValue("Anniversary", ref anniversary, value);
      }
    }
    Gender gender;
    public Gender Gender
    {
      get
      {
        return gender;
      }
      set
      {
        SetPropertyValue("Gender", ref gender, value);
      }
    }
    MaritalStatus maritalStatus;
    public MaritalStatus MaritalStatus
    {
      get
      {
        return maritalStatus;
      }
      set
      {
        SetPropertyValue("MaritalStatus", ref maritalStatus, value);
      }
    }
    string mobilePhone;
    [Size(SizeAttribute.DefaultStringMappingFieldSize)]
    public string MobilePhone
    {
      get
      {
        return mobilePhone;
      }
      set
      {
        SetPropertyValue("MobilePhone", ref mobilePhone, value);
      }
    }
    Customer account;
    [Association("Customer-Contacts")]
    public Customer Account
    {
      get
      {
        return account;
      }
      set
      {
        SetPropertyValue("Customer", ref account, value);
      }
    }
  }
}