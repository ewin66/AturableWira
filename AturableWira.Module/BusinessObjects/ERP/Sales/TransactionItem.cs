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

namespace AturableWira.Module.BusinessObjects.ERP.Sales
{
    [DefaultClassOptions]
    [NavigationItem(false)]
    [CreatableItem(false)]
    [ImageName("BO_Product")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class TransactionItem : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public TransactionItem(Session session)
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
        Item item;
        [RuleRequiredField]
        [ImmediatePostData]
        [DataSourceCriteria("[Discontinued]=false")]
        public Item Item
        {
            get
            {
                return item;
            }
            set
            {
                SetPropertyValue("Item", ref item, value);
            }
        }
        decimal unitPrice;
        public decimal UnitPrice
        {
            get
            {
                return unitPrice;
            }
            set
            {
                SetPropertyValue("UnitPrice", ref unitPrice, value);
            }
        }
        int quantity;
        [ImmediatePostData]
        [RuleValueComparison(ValueComparisonType.GreaterThan, 0)]
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                SetPropertyValue("Quantity", ref quantity, value);
            }
        }
        decimal discountAmount;
        [ImmediatePostData]
        public decimal DiscountAmount
        {
            get
            {
                return discountAmount;
            }
            set
            {
                SetPropertyValue("Discount", ref discountAmount, value);
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
        [PersistentAlias("(Quantity * Item.Cost) - DiscountAmount - ((Quantity * Item.Cost) * (DiscountPercent/100))")]
        public decimal Amount
        {
            get
            {
                if (Item != null)
                    return (decimal)EvaluateAlias("Amount");
                else
                    return 0;
            }
        }
        Transaction transaction;
        [Association("Transaction-Items")]
        public Transaction Transaction
        {
            get
            {
                return transaction;
            }
            set
            {
                SetPropertyValue("Transaction", ref transaction, value);
            }
        }
    }
}