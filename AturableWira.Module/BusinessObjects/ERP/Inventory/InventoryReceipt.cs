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
using AturableWira.Module.BusinessObjects.ERP.Purchase;
using System.Collections;
using AturableWira.Module.BusinessObjects.ACC.AP;
using AturableWira.Module.BusinessObjects.SYS;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace AturableWira.Module.BusinessObjects.ERP.Inventory
{
    [DefaultClassOptions]
    
    //[ImageName("BO_Contact")]
    [DefaultProperty("ReceiptNumber")]
    [ModelDefault("Caption", "Receipt")]
    [NavigationItem("Purchasing")]
    [Appearance("InventoryReceiptAppearancePosted", Criteria = "Posted = true", Enabled = false, TargetItems = "*")]
    [Appearance("DisableDeletePosted", Criteria = "Posted = true", AppearanceItemType = "Action", TargetItems = "Delete", Enabled = false)]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class InventoryReceipt : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public InventoryReceipt(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).

            Date = DateTime.Now;
            PeriodMonth = DateTime.Now.Month;
            PeriodYear = DateTime.Now.Year;

            using (UnitOfWork uow = new UnitOfWork(Session.DataLayer))
            {
                SystemSetting setting = uow.FindObject<SystemSetting>(null);
                setting.InventoryReceiptNumber += 1;
                string recNum = setting.InventoryReceiptNumber.ToString();
                ReceiptNumber = recNum;
                uow.CommitChanges();
            }
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
        string receiptNumber;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField,RuleUniqueValue,Key]
        [VisibleInListView(true)]
        public string ReceiptNumber
        {
            get
            {
                return receiptNumber;
            }
            set
            {
                SetPropertyValue("ReceiptNumber", ref receiptNumber, value);
            }
        }

        APInvoice aPInvoice;
        [ModelDefault("Caption", "AP Invoice")]
        [VisibleInDetailView(false), VisibleInListView(true)]
        public APInvoice APInvoice
        {
            get
            {
                return aPInvoice;
            }
            set
            {
                SetPropertyValue("APInvoice", ref aPInvoice, value);
            }
        }

        PurchaseOrder purchaseOrder;
        [Association("PurchaseOrder-Receipts")]
        [ImmediatePostData]
        public PurchaseOrder PurchaseOrder
        {
            get
            {
                return purchaseOrder;
            }
            set
            {
                if (SetPropertyValue("PurchaseOrder", ref purchaseOrder, value))
                    if (!IsLoading)
                    {
                        ArrayList objectsToDelete = new ArrayList();
                        foreach (InventoryReceiptItem item in Items)
                        {
                            objectsToDelete.Add(item);
                        }
                        Session.Delete(objectsToDelete);
                        if (PurchaseOrder != null)
                            foreach (OrderItem item in purchaseOrder.Items)
                            {
                                InventoryReceiptItem inventory = new InventoryReceiptItem(Session);
                                inventory.OrderItem = item;
                                inventory.VendorItem = Session.GetObjectByKey<VendorItem>(item.VendorItem.Oid);
                                inventory.UnitCost = item.VendorItem.OurItemCost;
                                inventory.QuantityReceived = item.Quantity;
                                inventory.TemporaryQuantity = item.Quantity - item.Received;

                                Items.Add(inventory);
                            }
                    }
            }
        }
        DateTime date;
        [RuleRequiredField]
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                SetPropertyValue("Date", ref date, value);
            }
        }
        int periodMonth;
        [RuleRange(1, 12)]
        [ModelDefault("Caption", "Month")]
        public int PeriodMonth
        {
            get
            {
                return periodMonth;
            }
            set
            {
                SetPropertyValue("PeriodMonth", ref periodMonth, value);
            }
        }
        int periodYear;
        [ModelDefault("DisplayFormat", "{0:d0}")]
        [ModelDefault("EditMask", "d0")]
        [ModelDefault("Caption", "Year")]
        public int PeriodYear
        {
            get
            {
                return periodYear;
            }
            set
            {
                SetPropertyValue("PeriodYear", ref periodYear, value);
            }
        }
        bool posted;
        public bool Posted
        {
            get
            {
                return posted;
            }
            set
            {
                SetPropertyValue("Posted", ref posted, value);
            }
        }

        [Association("InventoryReceipt-Items"), Aggregated]
        public XPCollection<InventoryReceiptItem> Items
        {
            get
            {
                return GetCollection<InventoryReceiptItem>("Items");
            }
        }
    }
}