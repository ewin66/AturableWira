namespace AturableWira.Module.Controllers
{
    partial class JVViewController
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
            this.JVPostAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // JVPostAction
            // 
            this.JVPostAction.Caption = "Post";
            this.JVPostAction.ConfirmationMessage = null;
            this.JVPostAction.Id = "JVPostAction";
            this.JVPostAction.TargetObjectsCriteria = "Posted=false";
            this.JVPostAction.TargetObjectType = typeof(AturableWira.Module.BusinessObjects.ACC.GL.JournalVoucher);
            this.JVPostAction.ToolTip = null;
            this.JVPostAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.JVPostAction_Execute);
            // 
            // JVViewController
            // 
            this.Actions.Add(this.JVPostAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction JVPostAction;
    }
}
