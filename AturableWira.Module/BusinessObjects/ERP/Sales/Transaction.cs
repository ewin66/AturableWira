using AturableWira.Module.BusinessObjects.CRM;
using AturableWira.Module.BusinessObjects.HRM;
using AturableWira.Module.BusinessObjects.SYS;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace AturableWira.Module.BusinessObjects.ERP.Sales
{
   [DefaultClassOptions]
   [CreatableItem(false)]
   [NavigationItem(false)]
   [Appearance("ShippingAppearanceRule", Criteria = "[DifferBillingAddress]=False", TargetItems = "BillStreet,BillCountry,BillProvince,BillRegion,BillSubRegion,BillPostalCode",Enabled = false)]
   [Appearance("BillingAppearanceRule", Criteria = "[DifferShippingAddress]=False", TargetItems = "ShipStreet,ShipCountry,ShipProvince,ShipRegion,ShipSubRegion,ShipPostalCode", Enabled = false)]
   //[ImageName("BO_Contact")]
   //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   public class Transaction : BaseObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public Transaction(Session session)
          : base(session)
      {
      }
      public override void AfterConstruction()
      {
         base.AfterConstruction();
         // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
         TransactionTime = DateTime.Now;
         Owner = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
      }
      protected override void OnLoaded()
      {
         base.OnLoaded();
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
      string referenceNumber;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      public string ReferenceNumber
      {
         get
         {
            return referenceNumber;
         }
         set
         {
            SetPropertyValue("ReferenceNumber", ref referenceNumber, value);
         }
      }
      DateTime transactionTime;
      [ModelDefault("DisplayFormat", "{0:g}")]
      [ModelDefault("EditMask", "g")]
      public DateTime TransactionTime
      {
         get
         {
            return transactionTime;
         }
         set
         {
            SetPropertyValue("TransactionTime", ref transactionTime, value);
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
      Customer customer;
      [ImmediatePostData(true)]
      [DataSourceCriteria("[Status]=1")]
      [RuleRequiredField]
      public Customer Customer
      {
         get
         {
            return customer;
         }
         set
         {
            SetPropertyValue("Customer", ref customer, value);
         }
      }

      bool differShippingAddress;
      [ImmediatePostData(true)]
      [ModelDefault("Caption", "Use Different Shipping Address")]
      public bool DifferShippingAddress
      {
         get
         {
            return differShippingAddress;
         }
         set
         {
            SetPropertyValue("DifferShippingAddress", ref differShippingAddress, value);
         }
      }
      string shipStreet;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      [ImmediatePostData]
      [ModelDefault("Caption", "Street")]
      public string ShipStreet
      {
         get
         {
            return shipStreet;
         }
         set
         {
            SetPropertyValue("ShipStreet", ref shipStreet, value);
         }
      }

      SYS.Country shipCountry;
      [ImmediatePostData]
      [ModelDefault("Caption", "Country")]
      public SYS.Country ShipCountry
      {
         get
         {
            return shipCountry;
         }
         set
         {
            SetPropertyValue("ShipCountry", ref shipCountry, value);
         }
      }
      Province shipProvince;
      [ImmediatePostData]
      [ModelDefault("Caption", "Province")]
      [DataSourceProperty("ShipCountry.Provinces")]
      public Province ShipProvince
      {
         get
         {
            return shipProvince;
         }
         set
         {
            SetPropertyValue("ShipProvince", ref shipProvince, value);
         }
      }
      Region shipRegion;
      [ImmediatePostData]
      [ModelDefault("Caption", "Region")]
      [DataSourceProperty("ShipProvince.Regions")]
      public Region ShipRegion
      {
         get
         {
            return shipRegion;
         }
         set
         {
            SetPropertyValue("ShipRegion", ref shipRegion, value);
         }
      }
      SubRegion shipSubRegion;
      [DataSourceProperty("ShipRegion.SubRegions")]
      [ImmediatePostData]
      [ModelDefault("Caption", "Sub Region")]
      public SubRegion ShipSubRegion
      {
         get
         {
            return shipSubRegion;
         }
         set
         {
            SetPropertyValue("ShipSubRegion", ref shipSubRegion, value);
         }
      }
      string shipPostalCode;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      [ImmediatePostData]
      [ModelDefault("Caption", "Postal Code")]
      public string ShipPostalCode
      {
         get
         {
            return shipPostalCode;
         }
         set
         {
            SetPropertyValue("ShipPostalCode", ref shipPostalCode, value);
         }
      }
      private const string shipDefaultFullAddressFormat = "{ShipStreet}, {SubRegion}, {Region}, {Province}, {Country}, {PostalCode}";
      private static string shipFullAddressFormat = shipDefaultFullAddressFormat;
      [VisibleInDetailView(false)]
      public static string ShipFullAddressFormat
      {
         get
         {
            return shipFullAddressFormat;
         }
         set
         {
            shipFullAddressFormat = value;
            if (string.IsNullOrEmpty(shipFullAddressFormat))
            {
               shipFullAddressFormat = shipDefaultFullAddressFormat;
            }
         }
      }
      [VisibleInDetailView(false)]
      [ModelDefault("Caption", "Shipping Address")]
      public string ShipFullAddress
      {
         get
         {
            return ObjectFormatter.Format(shipDefaultFullAddressFormat, this,
               EmptyEntriesMode.RemoveDelimiterWhenEntryIsEmpty);
         }
      }

      bool differBillingAddress;
      [ImmediatePostData(true)]
      [ModelDefault("Caption", "Use Different Billing Address")]
      public bool DifferBillingAddress
      {
         get
         {
            return differBillingAddress;
         }
         set
         {
            SetPropertyValue("DifferBillingAddress", ref differBillingAddress, value);
         }
      }
      string billStreet;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      [ImmediatePostData]
      [ModelDefault("Caption", "Street")]
      public string BillStreet
      {
         get
         {
            return billStreet;
         }
         set
         {
            SetPropertyValue("BillStreet", ref billStreet, value);
         }
      }

      SYS.Country billCountry;
      [ImmediatePostData]
      [ModelDefault("Caption", "Country")]
      public SYS.Country BillCountry
      {
         get
         {
            return billCountry;
         }
         set
         {
            SetPropertyValue("BillCountry", ref billCountry, value);
         }
      }
      Province billProvince;
      [ImmediatePostData]
      [ModelDefault("Caption", "Province")]
      [DataSourceProperty("BillCountry.Provinces")]
      public Province BillProvince
      {
         get
         {
            return billProvince;
         }
         set
         {
            SetPropertyValue("BillProvince", ref billProvince, value);
         }
      }
      Region billRegion;
      [ImmediatePostData]
      [DataSourceProperty("BillProvince.Regions")]
      [ModelDefault("Caption", "Region")]
      public Region BillRegion
      {
         get
         {
            return billRegion;
         }
         set
         {
            SetPropertyValue("BillRegion", ref billRegion, value);
         }
      }
      SubRegion billSubRegion;
      [DataSourceProperty("BillRegion.SubRegions")]
      [ImmediatePostData]
      [ModelDefault("Caption", "Sub Region")]
      public SubRegion BillSubRegion
      {
         get
         {
            return billSubRegion;
         }
         set
         {
            SetPropertyValue("BillSubRegion", ref billSubRegion, value);
         }
      }
      string billPostalCode;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      [ImmediatePostData]
      [ModelDefault("Caption", "Postal Code")]
      public string BillPostalCode
      {
         get
         {
            return billPostalCode;
         }
         set
         {
            SetPropertyValue("BillPostalCode", ref billPostalCode, value);
         }
      }
      private const string billDefaultFullAddressFormat = "{BillStreet}, {SubRegion}, {Region}, {Province}, {Country}, {PostalCode}";
      private static string billFullAddressFormat = billDefaultFullAddressFormat;
      [VisibleInDetailView(false)]
      public static string BillFullAddressFormat
      {
         get
         {
            return billFullAddressFormat;
         }
         set
         {
            billFullAddressFormat = value;
            if (string.IsNullOrEmpty(billFullAddressFormat))
            {
               billFullAddressFormat = billDefaultFullAddressFormat;
            }
         }
      }
      [VisibleInDetailView(false)]
      [ModelDefault("Caption", "Billing Address")]
      public string BillFullAddress
      {
         get
         {
            return ObjectFormatter.Format(billDefaultFullAddressFormat, this,
               EmptyEntriesMode.RemoveDelimiterWhenEntryIsEmpty);
         }
      }

      [PersistentAlias("Items.Sum(Amount)")]
      [ModelDefault("DisplayFormat", "{0:n2}")]
      public decimal SubTotal
      {
         get
         {
            if (Items.Count > 0)
               return (decimal)EvaluateAlias("SubTotal");
            else return 0;
         }
      }

      [PersistentAlias("Items.Sum(Item.ItemWeight)")]
      public double TotalWeight
      {
         get
         {
            if (Items.Count > 0)
               return (double)EvaluateAlias("TotalWeight");
            else
               return 0;
         }
      }

      ShippingMethod shippingMethod;
      [DataSourceCriteria("[IsActive]=true")]
      public ShippingMethod ShippingMethod
      {
         get
         {
            return shippingMethod;
         }
         set
         {
            SetPropertyValue("ShippingMethod", ref shippingMethod, value);
         }
      }

      decimal shippingCost;
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "N2")]
      public decimal ShippingCost
      {
         get
         {
            return shippingCost;
         }
         set
         {
            SetPropertyValue("ShippingCost", ref shippingCost, value);
         }
      }

      decimal discountAmount;
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "N2")]
      public decimal DiscountAmount
      {
         get
         {
            return discountAmount;
         }
         set
         {
            SetPropertyValue("DiscountAmount", ref discountAmount, value);
         }
      }

      decimal discountPercent;
      [ModelDefault("DisplayFormat", "{0:N2}%")]
      [ModelDefault("EditMask", "N2")]
      public decimal DiscountPercent
      {
         get
         {
            return discountPercent;
         }
         set
         {
            SetPropertyValue("DiscountPercent", ref discountPercent, value);
         }
      }

      [PersistentAlias("SubTotal - DiscountAmount - (SubTotal * (DiscountPercent/100))")]
      [ModelDefault("DisplayFormat", "{0:n2}")]
      [ModelDefault("EditMask", "n2")]
      public decimal Total
      {
         get
         {
            return (decimal)EvaluateAlias("Total");
         }
      }

      [Association("Transaction-Items"), Aggregated]
      public XPCollection<TransactionItem> Items
      {
         get
         {
            return GetCollection<TransactionItem>("Items");
         }
      }
      [Association("Transaction-Notes"), Aggregated]
      public XPCollection<TransactionNote> Notes
      {
         get
         {
            return GetCollection<TransactionNote>("Notes");
         }
      }
   }
}