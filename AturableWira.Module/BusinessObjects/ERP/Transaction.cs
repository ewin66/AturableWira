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
using AturableWira.Module.BusinessObjects.CRM;

namespace AturableWira.Module.BusinessObjects.ERP
{
    [DefaultClassOptions]
    [CreatableItem(false)]
    [NavigationItem(false)]
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
            Employee = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
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
        string transactionRef;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string TransactionRef
        {
            get
            {
                return transactionRef;
            }
            set
            {
                SetPropertyValue("TransactionRef", ref transactionRef, value);
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
        Employee employee;
        public Employee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                SetPropertyValue("Employee", ref employee, value);
            }
        }
        Company company;
        [ImmediatePostData(true)]
        public Company Company
        {
            get
            {
                return company;
            }
            set
            {
                SetPropertyValue("Company", ref company, value);
            }
        }
        Contact contact;
        [DataSourceProperty("Company.Contacts")]
        public Contact Contact
        {
            get
            {
                return contact;
            }
            set
            {
                SetPropertyValue("Contact", ref contact, value);
            }
        }
        [PersistentAlias("Items.Sum(Amount)")]
        public decimal SubTotal
        {
            get
            {
                if (Items.Count > 0)
                {
                    return (decimal)EvaluateAlias("SubTotal");
                }
                else { return 0; }
                
            }
        }

        decimal discountAmount;
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
    }
}