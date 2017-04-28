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
using AturableWira.Module.BusinessObjects.CRM;
using AturableWira.Module.BusinessObjects.HRM;
using AturableWira.Module.BusinessObjects.ERP.Inventory;
using static AturableWira.Module.BusinessObjects.ETC.Enums;
using AturableWira.Module.BusinessObjects.SYS;

namespace AturableWira.Module.BusinessObjects.ERP.Purchase
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("OrderNumber")]
    [NavigationItem("Purchasing")]
    [ModelDefault("Caption", "Purchase Order")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class PurchaseOrder : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public PurchaseOrder(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            int orderNum = 0;
            using (UnitOfWork uow = new UnitOfWork(Session.DataLayer))
            {
                SystemSetting systemSetting = uow.FindObject<SystemSetting>(null);
                systemSetting.PurchaseOrderNumber += 1;
                orderNum = systemSetting.PurchaseOrderNumber;
                uow.CommitTransaction();
            }

            //Session tempSession = new Session();
            //tempSession.ConnectionString = this.Session.ConnectionString;
            //SystemSetting setting = tempSession.FindObject<SystemSetting>(null);
            //setting.PurchaseOrderNumber += 1;
            //setting.Save();
            //tempSession.CommitTransaction();

            OrderedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            OderDate = DateTime.Now;
            OrderNumber = orderNum.ToString();
            RequestedReceipt = DateTime.Now.AddDays(1);
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
        string orderNumber;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField, RuleUniqueValue, Key]
        [VisibleInListView(true)]
        public string OrderNumber
        {
            get
            {
                return orderNumber;
            }
            set
            {
                SetPropertyValue("OrderNumber", ref orderNumber, value);
            }
        }

        OrderStatus orderStatus;
        public OrderStatus OrderStatus
        {
            get
            {
                return orderStatus;
            }
            set
            {
                SetPropertyValue("OrderStatus", ref orderStatus, value);
            }
        }

        Vendor vendor;
        public Vendor Vendor
        {
            get
            {
                return vendor;
            }
            set
            {
                SetPropertyValue("Vendor", ref vendor, value);
            }
        }

        DateTime oderDate;
        public DateTime OderDate
        {
            get
            {
                return oderDate;
            }
            set
            {
                SetPropertyValue("OderDate", ref oderDate, value);
            }
        }
        DateTime requestedReceipt;
        public DateTime RequestedReceipt
        {
            get
            {
                return requestedReceipt;
            }
            set
            {
                SetPropertyValue("RequestedReceipt", ref requestedReceipt, value);
            }
        }

        Employee orderedBy;
        public Employee OrderedBy
        {
            get
            {
                return orderedBy;
            }
            set
            {
                SetPropertyValue("OrderedBy", ref orderedBy, value);
            }
        }

        Warehouse warehouse;
        public Warehouse Warehouse
        {
            get
            {
                return warehouse;
            }
            set
            {
                SetPropertyValue("Warehouse", ref warehouse, value);
            }
        }

        Freight freight;
        public Freight Freight
        {
            get
            {
                return freight;
            }
            set
            {
                SetPropertyValue("Freight", ref freight, value);
            }
        }

        Carrier carrier;
        public Carrier Carrier
        {
            get
            {
                return carrier;
            }
            set
            {
                SetPropertyValue("Carrier", ref carrier, value);
            }
        }

        [PersistentAlias("Items.Sum(Amount)")]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        [ModelDefault("EditMask", "n2")]
        public decimal Amount
        {
            get
            {
                if (Items.Count > 0)
                    return (decimal)EvaluateAlias("Amount");
                else return 0;
            }
        }

        string comments;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Comments
        {
            get
            {
                return comments;
            }
            set
            {
                SetPropertyValue("Comments", ref comments, value);
            }
        }

        [Association("PurchaseOrder-Items"), Aggregated]
        public XPCollection<OrderItem> Items
        {
            get
            {
                return GetCollection<OrderItem>("Items");
            }
        }

        [Association("PurchaseOrder-Receipts")]
        public XPCollection<InventoryReceipt> Receipts
        {
            get
            {
                return GetCollection<InventoryReceipt>("Receipts");
            }
        }
    }
}