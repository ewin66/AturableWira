namespace AturableWira.Module.Mobile {
    partial class AturableWiraMobileModule {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            // 
            // AturableWiraMobileModule
            // 
            this.RequiredModuleTypes.Add(typeof(AturableWira.Module.AturableWiraModule));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Mobile.SystemModule.SystemMobileModule));
			this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.FileAttachments.Mobile.FileAttachmentsMobileModule));
			this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ReportsV2.Mobile.ReportsMobileModuleV2));
        }

        #endregion
    }
}