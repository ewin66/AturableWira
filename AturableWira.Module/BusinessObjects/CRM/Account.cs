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
using AturableWira.Module.BusinessObjects.HRM;
using AturableWira.Module.BusinessObjects.SYS;
using static AturableWira.Module.BusinessObjects.ETC.Enums;
using AturableWira.Module.BusinessObjects.ERP.Sales;
using AturableWira.Module.BusinessObjects.ACC;

namespace AturableWira.Module.BusinessObjects.CRM
{
   [DefaultClassOptions]
   [NavigationItem(false)]
   [CreatableItem(false)]
   //[ImageName("BO_Contact")]
   //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   public class Account : BaseObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public Account(Session session)
          : base(session)
      {
      }
      public override void AfterConstruction()
      {
         base.AfterConstruction();
         // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
         status = CustomerStatus.Active;
         Owner = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
         SystemSetting setting = Session.FindObject<SystemSetting>(null);
         CreditLimit = setting.CreditLimit;
         Currency = setting.DefaultCurrency;
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

      CustomerStatus status;
      public CustomerStatus Status
      {
         get
         {
            return status;
         }
         set
         {
            SetPropertyValue("Status", ref status, value);
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

      string street;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      [ImmediatePostData]
      public string Street
      {
         get
         {
            return street;
         }
         set
         {
            SetPropertyValue("Street", ref street, value);
         }
      }

      SYS.Country country;
      [ImmediatePostData]
      public SYS.Country Country
      {
         get
         {
            return country;
         }
         set
         {
            SetPropertyValue("Country", ref country, value);
         }
      }
      Province province;
      [ImmediatePostData]
      [DataSourceProperty("Country.Provinces")]
      public Province Province
      {
         get
         {
            return province;
         }
         set
         {
            SetPropertyValue("Province", ref province, value);
         }
      }
      Region region;
      [ImmediatePostData]
      [DataSourceProperty("Province.Regions")]
      public Region Region
      {
         get
         {
            return region;
         }
         set
         {
            SetPropertyValue("Region", ref region, value);
         }
      }
      SubRegion subRegion;
      [DataSourceProperty("Region.SubRegions")]
      [ImmediatePostData]
      public SubRegion SubRegion
      {
         get
         {
            return subRegion;
         }
         set
         {
            SetPropertyValue("SubRegion", ref subRegion, value);
         }
      }
      string postalCode;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      [ImmediatePostData]
      public string PostalCode
      {
         get
         {
            return postalCode;
         }
         set
         {
            SetPropertyValue("PostalCode", ref postalCode, value);
         }
      }
      private const string defaultFullAddressFormat = "{Street}, {SubRegion}, {Region}, {Province}, {Country}, {PostalCode}";
      private static string fullAddressFormat = defaultFullAddressFormat;
      [VisibleInDetailView(false)]
      public static string FullAddressFormat
      {
         get { return fullAddressFormat; }
         set
         {
            fullAddressFormat = value;
            if (string.IsNullOrEmpty(fullAddressFormat))
            {
               fullAddressFormat = defaultFullAddressFormat;
            }
         }
      }
      [VisibleInDetailView(false)]
      public string FullAddress
      {
         get
         {
            return ObjectFormatter.Format(fullAddressFormat, this,
               EmptyEntriesMode.RemoveDelimiterWhenEntryIsEmpty);
         }
      }
      string email;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      public string Email
      {
         get
         {
            return email;
         }
         set
         {
            SetPropertyValue("Email", ref email, value);
         }
      }
      string homePhone;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      public string HomePhone
      {
         get
         {
            return homePhone;
         }
         set
         {
            SetPropertyValue("HomePhone", ref homePhone, value);
         }
      }
      string officePhone;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      public string OfficePhone
      {
         get
         {
            return officePhone;
         }
         set
         {
            SetPropertyValue("OfficePhone", ref officePhone, value);
         }
      }
      string fax;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      public string Fax
      {
         get
         {
            return fax;
         }
         set
         {
            SetPropertyValue("Fax", ref fax, value);
         }
      }
      string otherPhone;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      public string OtherPhone
      {
         get
         {
            return otherPhone;
         }
         set
         {
            SetPropertyValue("OtherPhone", ref otherPhone, value);
         }
      }
      string website;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      public string Website
      {
         get
         {
            return website;
         }
         set
         {
            SetPropertyValue("Website", ref website, value);
         }
      }
      string facebook;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      public string Facebook
      {
         get
         {
            return facebook;
         }
         set
         {
            SetPropertyValue("Facebook", ref facebook, value);
         }
      }
      string twitter;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      public string Twitter
      {
         get
         {
            return twitter;
         }
         set
         {
            SetPropertyValue("Twitter", ref twitter, value);
         }
      }
      string linkedIn;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      public string LinkedIn
      {
         get
         {
            return linkedIn;
         }
         set
         {
            SetPropertyValue("LinkedIn", ref linkedIn, value);
         }
      }

      Currency currency;
      [RuleRequiredField]
      public Currency Currency
      {
         get
         {
            return currency;
         }
         set
         {
            SetPropertyValue("Currency", ref currency, value);
         }
      }

      XPCollection<Invoice> invoices;
      public XPCollection<Invoice> Invoices
      {
         get
         {
            if (invoices == null)
            {
               invoices = new XPCollection<Invoice>(Session, CriteriaOperator.Parse("[Customer.Oid] = ?", this.Oid));
            } else
            {
               invoices = null;
            }
            return invoices;
         }
      }

      XPCollection<Quote> quotes;
      public XPCollection<Quote> Quotes
      {
         get
         {
            if (quotes == null)
            {
               quotes = new XPCollection<Quote>(Session, CriteriaOperator.Parse("[Customer.Oid] = ?", this.Oid));
            }
            else
            {
               quotes = null;
            }
            return quotes;
         }
      }

      XPCollection<Order> orders;
      public XPCollection<Order> Orders
      {
         get
         {
            if (orders == null)
            {
               orders = new XPCollection<Order>(Session, CriteriaOperator.Parse("[Customer.Oid] = ?", this.Oid));
            }
            else
            {
               orders = null;
            }
            return orders;
         }
      }

      [Association("Account-Notes"), Aggregated]
      public XPCollection<AccountNote> Notes
      {
         get
         {
            return GetCollection<AccountNote>("Notes");
         }
      }
   }
}