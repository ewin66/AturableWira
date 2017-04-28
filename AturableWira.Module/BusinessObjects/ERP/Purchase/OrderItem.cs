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
using DevExpress.ExpressApp.Editors;
using System.Collections;
using AturableWira.Module.BusinessObjects.ERP.Inventory;

namespace AturableWira.Module.BusinessObjects.ERP.Purchase
{
    [DefaultClassOptions]
    [NavigationItem(false)]
    [CreatableItem(false)]
    [ImageName("BO_Product")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class OrderItem : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public OrderItem(Session session)
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

        PurchaseOrder purchaseOrder;
        [Association("PurchaseOrder-Items")]
        public PurchaseOrder PurchaseOrder
        {
            get
            {
                return purchaseOrder;
            }
            set
            {
                SetPropertyValue("PurchaseOrder", ref purchaseOrder, value);
            }
        }
        int quantity;
        [RuleValueComparison(ValueComparisonType.GreaterThanOrEqual, 1)]
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
        VendorItem vendorItem;
        [ImmediatePostData]
        [RuleRequiredField]
        [DataSourceProperty("PurchaseOrder.Vendor.Items")]
        public VendorItem VendorItem
        {
            get
            {
                return vendorItem;
            }
            set
            {
                if (SetPropertyValue("VendorItem", ref vendorItem, value))
                    if (!IsLoading && VendorItem != null)
                        UnitCost = VendorItem.Cost;
            }
        }

        decimal unitCost;
        [ModelDefault("DisplayFormat", "{0:n2}")]
        [ModelDefault("EditMask", "n2")]
        [ImmediatePostData]
        public decimal UnitCost
        {
            get
            {
                return unitCost;
            }
            set
            {
                SetPropertyValue("UnitCost", ref unitCost, value);
            }
        }

        [PersistentAlias("UnitCost * Quantity")]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        [ModelDefault("EditMask",  "n2")]
        public decimal Amount
        {
            get
            {
                return (decimal)EvaluateAlias("Amount");
            }
        }

        string notes;
        [Size(SizeAttribute.Unlimited)]
        [ModelDefault("RowCount", "1")]
        [EditorAlias(EditorAliases.StringPropertyEditor)]
        public string Notes
        {
            get
            {
                return notes;
            }
            set
            {
                SetPropertyValue("Notes", ref notes, value);
            }
        }

        public int Received
        {
            get
            {
                int received = 0;

                ICollection inventory;
                inventory = Session.GetObjects(Session.GetClassInfo(typeof(Inventory.InventoryReceiptItem)), CriteriaOperator.Parse("OrderItem.Oid=?", Oid), new SortingCollection(null), 0, false, true);

                foreach(Inventory.InventoryReceiptItem item in inventory)
                {
                    received += item.QuantityReceived;
                }
                return received;
            }
        }
    }
}