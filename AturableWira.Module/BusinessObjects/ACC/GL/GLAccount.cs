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
using static AturableWira.Module.BusinessObjects.ETC.Enums;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace AturableWira.Module.BusinessObjects.ACC.GL
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "GL Account")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [Appearance("GLAccountAppearance", "[AccountType] <> 'Equity'", TargetItems = "RetainedEarnings", Enabled = false)]
    public class GLAccount : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public GLAccount(Session session)
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
        string accountNumber;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField, RuleUniqueValue]
        [VisibleInLookupListView(true), VisibleInListView(true)]
        [Key]
        public string AccountNumber
        {
            get
            {
                return accountNumber;
            }
            set
            {
                SetPropertyValue("AccountNumber", ref accountNumber, value);
            }
        }
        string accountName;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        public string AccountName
        {
            get
            {
                return accountName;
            }
            set
            {
                SetPropertyValue("AccountName", ref accountName, value);
            }
        }
        bool suspended;
        [VisibleInLookupListView(false)]
        [ModelDefault("ToolTip", "Suspended account will not be selectable on new transactions")]
        public bool Suspended
        {
            get
            {
                return suspended;
            }
            set
            {
                SetPropertyValue("Suspended", ref suspended, value);
            }
        }
        GLACcountType accountType;
        [ImmediatePostData]
        public GLACcountType AccountType
        {
            get
            {
                return accountType;
            }
            set
            {
                if (SetPropertyValue("AccountType", ref accountType, value))
                    if (!IsLoading)
                        if (AccountType != GLACcountType.Equity)
                            RetainedEarnings = false;
            }
        }
        bool retainedEarnings;
        [VisibleInLookupListView(false)]
        public bool RetainedEarnings
        {
            get
            {
                return retainedEarnings;
            }
            set
            {
                SetPropertyValue("RetainedEarnings", ref retainedEarnings, value);
            }
        }
        string notes;
        [Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.HtmlPropertyEditor)]
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
    }
}