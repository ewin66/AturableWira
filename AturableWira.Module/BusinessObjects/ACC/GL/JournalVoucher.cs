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

namespace AturableWira.Module.BusinessObjects.ACC.GL
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class JournalVoucher : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public JournalVoucher(Session session)
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
        int periodMonth;
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
        DateTime voucherDate;
        public DateTime VoucherDate
        {
            get
            {
                return voucherDate;
            }
            set
            {
                SetPropertyValue("VoucherDate", ref voucherDate, value);
            }
        }
        bool autoReverse;
        [ModelDefault("Caption", "Auto-Reverse")]
        public bool AutoReverse
        {
            get
            {
                return autoReverse;
            }
            set
            {
                SetPropertyValue("AutoReverse", ref autoReverse, value);
            }
        }
        string description;
        [Size(SizeAttribute.Unlimited)]
        [ModelDefault("RowCount", "1")]
        [EditorAlias(EditorAliases.StringPropertyEditor)]
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
        bool posted;
        [CaptionsForBoolValues("Posted", "Not Posted")]
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
        [PersistentAlias("Entries.Sum(IIF(Amount>0,Amount,0))")]
        public decimal Debit
        {
            get
            {
                    return (decimal)EvaluateAlias("Debit");
            }
        }

        [PersistentAlias("Entries.Sum(IIF(Amount<0,Amount,0))")]
        public decimal Credit
        {
            get
            {
                    return (decimal)EvaluateAlias("Credit");
            }
        }


        [Association("JournalVoucher-Entries"), Aggregated]
        public XPCollection<JournalEntry> Entries
        {
            get
            {
                return GetCollection<JournalEntry>("Entries");
            }
        }
    }
}