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
using DevExpress.ExpressApp.Editors;
using AturableWira.Module.BusinessObjects.ACC.GL;
using AturableWira.Module.BusinessObjects.SYS;
using AturableWira.Module.BusinessObjects.ACC;

namespace AturableWira.Module.BusinessObjects.ERP
{
    [DefaultClassOptions]
    [ImageName("BO_Product")]
    [DefaultProperty("Name")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Item : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Item(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            SystemSetting settings = Session.FindObject<SystemSetting>(null);
            Sales = settings.Sales;
            Inventory = settings.Inventory;
            CostofGoodsSold = settings.CostofGoodsSold;
            Backorderable = true;
            Purchased = true;
            Sold = true;
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

        decimal itemNumber;
        [RuleRequiredField, RuleUniqueValue, Key]
        [ModelDefault("DisplayFormat", "{0:d0}")]
        [ModelDefault("EditMask", "d0")]
        [VisibleInListView(true)]
        public decimal ItemNumber
        {
            get
            {
                return itemNumber;
            }
            set
            {
                SetPropertyValue("ItemNumber", ref itemNumber, value);
            }
        }

        string name;
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
        bool discontinued;
        [ModelDefault("ToolTip", "Discontinued items will not be selectable on new transactions")]
        public bool Discontinued
        {
            get
            {
                return discontinued;
            }
            set
            {
                SetPropertyValue("Discontinued", ref discontinued, value);
            }
        }
        MediaDataObject image;
        public MediaDataObject Image
        {
            get
            {
                return image;
            }
            set
            {
                SetPropertyValue("Image", ref image, value);
            }
        }
        string description;
        [Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.HtmlPropertyEditor)]
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                SetPropertyValue("Description", ref description, value);
            }
        }
        decimal cost;
        [ModelDefault("DisplayFormat","{0:n2}")]
        [ModelDefault("EditMask", "n2")]
        public decimal Cost
        {
            get
            {
                return cost;
            }
            set
            {
                SetPropertyValue("Cost", ref cost, value);
            }
        }

        //decimal weightedAvarageCost;
        //Todo: calculation
        [NonPersistent]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        [ModelDefault("EditMask", "n2")]
        [ModelDefault("ToolTip","Weighted Avarage Cost is calculated automatically")]
        public decimal WeightedAvarageCost
        {
            get
            {
                return 0;
            }
        }

        double itemWeight;
        public double ItemWeight
        {
            get
            {
                return itemWeight;
            }
            set
            {
                SetPropertyValue("ItemWeight", ref itemWeight, value);
            }
        }
        int minimumQuantity;
        public int MinimumQuantity
        {
            get
            {
                return minimumQuantity;
            }
            set
            {
                SetPropertyValue("MinimumQuantity", ref minimumQuantity, value);
            }
        }
        bool purchased;
        public bool Purchased
        {
            get
            {
                return purchased;
            }
            set
            {
                SetPropertyValue("Purchased", ref purchased, value);
            }
        }
        bool sold;
        public bool Sold
        {
            get
            {
                return sold;
            }
            set
            {
                SetPropertyValue("Sold", ref sold, value);
            }
        }
        bool backorderable;
        public bool Backorderable
        {
            get
            {
                return backorderable;
            }
            set
            {
                SetPropertyValue("Backorderable", ref backorderable, value);
            }
        }
        GLAccount sales;
        public GLAccount Sales
        {
            get
            {
                return sales;
            }
            set
            {
                SetPropertyValue("Sales", ref sales, value);
            }
        }

        GLAccount costofGoodsSold;
        [ModelDefault("Caption", "Cost of Goods Sold")]
        public GLAccount CostofGoodsSold
        {
            get
            {
                return costofGoodsSold;
            }
            set
            {
                SetPropertyValue("CostofGoodsSold", ref costofGoodsSold, value);
            }
        }

        GLAccount inventory;
        public GLAccount Inventory
        {
            get
            {
                return inventory;
            }
            set
            {
                SetPropertyValue("Inventory", ref inventory, value);
            }
        }

        [Association("Item-ApplicableTax")]
        public XPCollection<Tax> ApplicableTax
        {
            get
            {
                return GetCollection<Tax>("ApplicableTax");
            }
        }
    }
}