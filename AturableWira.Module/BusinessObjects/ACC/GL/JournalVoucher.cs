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
using DevExpress.ExpressApp.ConditionalAppearance;
using static AturableWira.Module.BusinessObjects.ETC.Enums;

namespace AturableWira.Module.BusinessObjects.ACC.GL
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [Appearance("JournalVoucherPostedDisabled",TargetItems ="Posted,JournalVoucherSource", Enabled = false)]
    [Appearance("DisableDeleteWhenPosted", Criteria = "Posted", AppearanceItemType = "Action", TargetItems = "Delete", Enabled = false)]
    [Appearance("DisableEditWhenPosted", Criteria = "Posted", AppearanceItemType = "ViewItem", TargetItems = "*", Enabled = false)]
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
            PeriodMonth = DateTime.Now.Month;
            PeriodYear = DateTime.Now.Year;
            VoucherDate = DateTime.Now;
            Source = JournalVoucherSource.GL;
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
        [RuleRange(1,12)]
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
        [RuleRequiredField]
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
        [ModelDefault("DisplayFormat", "{0:n2}")]
        public decimal Debit
        {
            get
            {
                object result;
                result = EvaluateAlias("Debit");
                if (result == null)
                    return 0;
                else
                    return Convert.ToDecimal(result);
            }
        }

        JournalVoucherSource source;
        JournalVoucherSource Source
        {
            get
            {
                return source;
            }
            set
            {
                SetPropertyValue("Source", ref source, value);
            }
        }

        [PersistentAlias("Entries.Sum(IIF(Amount<0,Amount,0))")]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        public decimal Credit
        {
            get
            {
                object result;
                result = EvaluateAlias("Credit");
                if (result == null)
                    return 0;
                else
                    return Convert.ToDecimal(result) * -1;
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

        [Browsable(false)]
        [RuleFromBoolProperty("JVMustBalance", DefaultContexts.Save,CustomMessageTemplate = "Debit and Credit value must be the same(balanced)")]
        public bool IsBalance
        {
            get
            {
                if (Debit - Credit != 0)
                    return false;
                else
                    return true;
            }
        }

        [Action(Caption = "Post", SelectionDependencyType = MethodActionSelectionDependencyType.RequireMultipleObjects, ConfirmationMessage = "Are you sure want to post selected JV?", TargetObjectsCriteria = "!Posted", TargetObjectsCriteriaMode = DevExpress.ExpressApp.Actions.TargetObjectsCriteriaMode.TrueForAll, AutoCommit = true)]
        public void Post()
        {
            Posted = true;
        }

        [Action(Caption = "Unpost", SelectionDependencyType = MethodActionSelectionDependencyType.RequireMultipleObjects, ConfirmationMessage = "Are you sure want to unpost selected JV?", TargetObjectsCriteria = "Posted", TargetObjectsCriteriaMode = DevExpress.ExpressApp.Actions.TargetObjectsCriteriaMode.TrueForAll, AutoCommit = true)]
        public void Unpost()
        {
            Posted = false;
        }
    }
}