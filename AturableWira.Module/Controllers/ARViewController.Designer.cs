namespace AturableWira.Module.Controllers
{
   partial class ARViewController
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
         this.GeneratePricesAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
         this.AdjustPriceAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
         // 
         // GeneratePricesAction
         // 
         this.GeneratePricesAction.Caption = "Generate Prices";
         this.GeneratePricesAction.ConfirmationMessage = "All prices already listed will be overwritten, do you want to continue?";
         this.GeneratePricesAction.Id = "GeneratePricesAction";
         this.GeneratePricesAction.TargetObjectType = typeof(AturableWira.Module.BusinessObjects.ERP.Sales.PriceList);
         this.GeneratePricesAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
         this.GeneratePricesAction.ToolTip = null;
         this.GeneratePricesAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
         this.GeneratePricesAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.GeneratePricesAction_Execute);
         // 
         // AdjustPriceAction
         // 
         this.AdjustPriceAction.AcceptButtonCaption = null;
         this.AdjustPriceAction.CancelButtonCaption = null;
         this.AdjustPriceAction.Caption = "Adjust Price";
         this.AdjustPriceAction.ConfirmationMessage = "This will adjust all prices in this list by percentage,\r\ndo you want to continue?" +
    "";
         this.AdjustPriceAction.Id = "AdjustPriceAction";
         this.AdjustPriceAction.TargetObjectType = typeof(AturableWira.Module.BusinessObjects.ERP.Sales.PriceList);
         this.AdjustPriceAction.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
         this.AdjustPriceAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
         this.AdjustPriceAction.ToolTip = null;
         this.AdjustPriceAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
         this.AdjustPriceAction.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.AdjustPriceAction_CustomizePopupWindowParams);
         this.AdjustPriceAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.AdjustPriceAction_Execute);
         // 
         // ARViewController
         // 
         this.Actions.Add(this.GeneratePricesAction);
         this.Actions.Add(this.AdjustPriceAction);

      }

      #endregion

      private DevExpress.ExpressApp.Actions.SimpleAction GeneratePricesAction;
      private DevExpress.ExpressApp.Actions.PopupWindowShowAction AdjustPriceAction;
   }
}
