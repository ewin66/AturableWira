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
using static AturableWira.Module.BusinessObjects.ETC.Enums;
using AturableWira.Module.BusinessObjects.CRM;

namespace AturableWira.Module.BusinessObjects.SYS
{
    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    [CurrentUserDisplayImage("Photo")]
    [CreatableItem(false)]
    [NavigationItem(false)]
    [DefaultProperty("FullName")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class People : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public People(Session session)
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
        [PersistentAlias("Concat([FirstName], ' ', [LastName])")]
        public string FullName
        {
            get
            {
                return (string)EvaluateAlias("FullName");
            }
        }
        string firstName;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [ImmediatePostData(true)]
        [RuleRequiredField]
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                SetPropertyValue("FirstName", ref firstName, value);
            }
        }
        string lastName;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [ImmediatePostData(true)]
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                SetPropertyValue("LastName", ref lastName, value);
            }
        }
        DateTime birthDate;
        public DateTime BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                SetPropertyValue("BirthDate", ref birthDate, value);
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
        string home;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Home
        {
            get
            {
                return home;
            }
            set
            {
                SetPropertyValue("Home", ref home, value);
            }
        }
        string work;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Work
        {
            get
            {
                return work;
            }
            set
            {
                SetPropertyValue("Work", ref work, value);
            }
        }
        string ext;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Ext
        {
            get
            {
                return ext;
            }
            set
            {
                SetPropertyValue("Ext", ref ext, value);
            }
        }
        string mobile1;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Mobile1
        {
            get
            {
                return mobile1;
            }
            set
            {
                SetPropertyValue("Mobile1", ref mobile1, value);
            }
        }
        string mobile2;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Mobile2
        {
            get
            {
                return mobile2;
            }
            set
            {
                SetPropertyValue("Mobile2", ref mobile2, value);
            }
        }
        string email1;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Email1
        {
            get
            {
                return email1;
            }
            set
            {
                SetPropertyValue("Email1", ref email1, value);
            }
        }
        string email2;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Email2
        {
            get
            {
                return email2;
            }
            set
            {
                SetPropertyValue("Email2", ref email2, value);
            }
        }
        string position;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Position
        {
            get
            {
                return position;
            }
            set
            {
                SetPropertyValue("Position", ref position, value);
            }
        }
    }
}