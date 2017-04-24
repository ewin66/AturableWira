namespace AturableWira.Module.Controllers
{
    partial class APViewController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CreateInvoiceAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.AddAllOutstandingInvoiceAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.PostOrderReceipt = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.PostAPInvoiceAcction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // CreateInvoiceAction
            // 
            this.CreateInvoiceAction.Caption = "Create Invoice";
            this.CreateInvoiceAction.ConfirmationMessage = "Invoices for all selected receipts will be created, are you sure?";
            this.CreateInvoiceAction.Id = "CreateInvoice";
            this.CreateInvoiceAction.TargetObjectsCriteria = "[Invoice] is null";
            this.CreateInvoiceAction.TargetObjectsCriteriaMode = DevExpress.ExpressApp.Actions.TargetObjectsCriteriaMode.TrueForAll;
            this.CreateInvoiceAction.TargetObjectType = typeof(AturableWira.Module.BusinessObjects.ERP.Inventory.InventoryReceipt);
            this.CreateInvoiceAction.ToolTip = null;
            this.CreateInvoiceAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.CreateInvoiceAction_Execute);
            // 
            // AddAllOutstandingInvoiceAction
            // 
            this.AddAllOutstandingInvoiceAction.Caption = "Add All Outstanding Invoice";
            this.AddAllOutstandingInvoiceAction.ConfirmationMessage = "This will replace any invoice that may have been added to the list\r\nAre you sure " +
    "want to continue?";
            this.AddAllOutstandingInvoiceAction.Id = "AddOutstandingInvoice";
            this.AddAllOutstandingInvoiceAction.TargetObjectsCriteria = "[Posted] = False And [Vendor] Is Not Null";
            this.AddAllOutstandingInvoiceAction.TargetObjectType = typeof(AturableWira.Module.BusinessObjects.ACC.AP.APPayment);
            this.AddAllOutstandingInvoiceAction.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.AddAllOutstandingInvoiceAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.AddAllOutstandingInvoiceAction.ToolTip = "Add all outstanding invoice of selected vendor.";
            this.AddAllOutstandingInvoiceAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.AddAllOutstandingInvoiceAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.AddAllOutstandingInvoiceAction_Execute);
            // 
            // PostOrderReceipt
            // 
            this.PostOrderReceipt.Caption = "Post";
            this.PostOrderReceipt.ConfirmationMessage = "Once receipt is posted it is imposible to unpost.\r\nYou must create a reverse rece" +
    "ipt to adjust.\r\nAre you sure want to post selected receipts?\r\n";
            this.PostOrderReceipt.Id = "PostOrderReceipt";
            this.PostOrderReceipt.TargetObjectType = typeof(AturableWira.Module.BusinessObjects.ERP.Inventory.InventoryReceipt);
            this.PostOrderReceipt.ToolTip = null;
            this.PostOrderReceipt.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.PostOrderReceipt.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.PostOrderReceipt_Execute);
            // 
            // PostAPInvoiceAcction
            // 
            this.PostAPInvoiceAcction.Caption = "Post";
            this.PostAPInvoiceAcction.ConfirmationMessage = "This action will post all selected invoices. Journal Vouchers will also created.\r" +
    "\nAre you sure want to continue?\r\n";
            this.PostAPInvoiceAcction.Id = "PostAPInvoiceAction";
            this.PostAPInvoiceAcction.TargetObjectsCriteria = "[Posted] = false";
            this.PostAPInvoiceAcction.TargetObjectType = typeof(AturableWira.Module.BusinessObjects.ACC.AP.APInvoice);
            this.PostAPInvoiceAcction.ToolTip = null;
            this.PostAPInvoiceAcction.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.PostAPInvoiceAcction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.PostAPInvoiceAcction_Execute);
            // 
            // APViewController
            // 
            this.Actions.Add(this.CreateInvoiceAction);
            this.Actions.Add(this.AddAllOutstandingInvoiceAction);
            this.Actions.Add(this.PostOrderReceipt);
            this.Actions.Add(this.PostAPInvoiceAcction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction CreateInvoiceAction;
        private DevExpress.ExpressApp.Actions.SimpleAction AddAllOutstandingInvoiceAction;
        private DevExpress.ExpressApp.Actions.SimpleAction PostOrderReceipt;
        private DevExpress.ExpressApp.Actions.SimpleAction PostAPInvoiceAcction;
    }
}
