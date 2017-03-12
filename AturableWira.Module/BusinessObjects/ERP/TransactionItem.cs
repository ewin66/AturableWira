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

namespace AturableWira.Module.BusinessObjects.ERP
{
    [DefaultClassOptions]
    [NavigationItem(false)]
    [CreatableItem(false)]
    //[ImageName("BO_Contact")]
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
        Product product;
        [RuleRequiredField]
        public Product Product
        {
            get
            {
                return product;
            }
            set
            {
                SetPropertyValue("Product", ref product, value);
            }
        }
        int quantity;
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
        double price;
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                SetPropertyValue("Price", ref price, value);
            }
        }
        [PersistentAlias("Quantity * Price")]
        public double SubTotal
        {
            get
            {
                object tempObject = EvaluateAlias("SubTotal");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else
                {
                    return 0;
                }
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
                Transaction oldTransaction = transaction;
                SetPropertyValue("Transaction", ref transaction, value);
                if (!IsLoading && !IsSaving && oldTransaction != transaction)
                {
                    oldTransaction = oldTransaction ?? transaction;
                    oldTransaction.UpdateSumSubTotal(true);
                }
            }
        }
    }
}