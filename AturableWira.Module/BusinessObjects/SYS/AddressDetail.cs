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

namespace AturableWira.Module.BusinessObjects.SYS
{
   [DefaultClassOptions]
   //[ImageName("BO_Contact")]
   [DefaultProperty("FullAddress")]
   [CreatableItem(false)]
   [NavigationItem(false)]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   public class AddressDetail : BaseObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public AddressDetail(Session session)
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
   }
}