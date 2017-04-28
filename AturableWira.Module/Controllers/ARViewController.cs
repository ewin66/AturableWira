using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using AturableWira.Module.BusinessObjects.ERP;
using System.Collections;
using AturableWira.Module.BusinessObjects.ERP.Sales;
using AturableWira.Module.BusinessObjects.ETC;

namespace AturableWira.Module.Controllers
{
   // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
   public partial class ARViewController : ViewController
   {
      public ARViewController()
      {
         InitializeComponent();
         // Target required Views (via the TargetXXX properties) and create their Actions.
      }
      protected override void OnActivated()
      {
         base.OnActivated();
         // Perform various tasks depending on the target View.
      }
      protected override void OnViewControlsCreated()
      {
         base.OnViewControlsCreated();
         // Access and customize the target View control.
      }
      protected override void OnDeactivated()
      {
         // Unsubscribe from previously subscribed events and release other references and resources.
         base.OnDeactivated();
      }

      private void GeneratePricesAction_Execute(object sender, SimpleActionExecuteEventArgs e)
      {
         ArrayList list = new ArrayList();
         PriceList currentList = (PriceList)View.CurrentObject;
         foreach (Price price in currentList.Prices)
         {
            list.Add(price);
         }

         ObjectSpace.Delete(list);

         IList items = ObjectSpace.GetObjects(typeof(Item), CriteriaOperator.Parse("Sold=true"));
         foreach (Item item in items)
         {
            Price price = ObjectSpace.CreateObject<Price>();
            price.Item = ObjectSpace.GetObjectByKey<Item>(item.ItemNumber);
            price.ListPrice = item.Cost;

            currentList.Prices.Add(price);
         }
      }

      private void AddCostByAction_Execute(object sender, ParametrizedActionExecuteEventArgs e)
      {

      }

      private void AdjustPriceAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
      {
         PriceList list = (PriceList)View.CurrentObject;
         foreach (Price price in list.Prices)
         {
            decimal adjustBy = ((AddjustPriceParameterObject)e.PopupWindow.View.CurrentObject).AdjustPriceBy;
            price.ListPrice = price.ListPrice + (price.ListPrice * (adjustBy / 100));
         }
      }

      private void AdjustPriceAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
      {
         IObjectSpace objectSpace = Application.CreateObjectSpace();
         e.View = Application.CreateDetailView(Application.CreateObjectSpace(), AddjustPriceParameterObject.CreateShortDescriptionParametersObject());
         ((DetailView)e.View).ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
      }
   }
}
