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
using AturableWira.Module.BusinessObjects.ACC.AP;
using AturableWira.Module.BusinessObjects.ERP.Inventory;
using AturableWira.Module.BusinessObjects.ERP.Purchase;
using AturableWira.Module.BusinessObjects.SYS;
using AturableWira.Module.BusinessObjects.ACC.GL;
using AturableWira.Module.BusinessObjects.CRM;
using System.Collections;
using AturableWira.Module.BusinessObjects.ERP.Sales;

namespace AturableWira.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class APViewController : ViewController
    {
        public APViewController()
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

        private void CreateInvoiceAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            int invCount = 0;
            foreach(InventoryReceipt receipt in View.SelectedObjects)
            {
                IObjectSpace os = Application.CreateObjectSpace();
                
                SystemSetting setting = os.FindObject<SystemSetting>(null);
                Int16 invNumber = (Int16)setting.InvoiceNumber;
                setting.InvoiceNumber += 1;
                os.CommitChanges();

                APInvoice invoice = ObjectSpace.CreateObject<APInvoice>();
                invoice.PurchaseOrder = ObjectSpace.GetObjectByKey<PurchaseOrder>(receipt.PurchaseOrder.OrderNumber);
                invoice.InvoiceNumber = invNumber.ToString();
                invoice.PeriodMonth = DateTime.Now.Month;
                invoice.PeriodYear = DateTime.Now.Year;
                invoice.Vendor = ObjectSpace.GetObjectByKey<Vendor>(receipt.PurchaseOrder.Vendor.Oid);
                invoice.InvoiceDate = DateTime.Now;
                invoice.Description = String.Format("Receipt: {0} Inventory", receipt.ReceiptNumber);
                receipt.APInvoice = invoice;

                foreach(Inventory inventory in receipt.Items)
                {
                    APInvoiceItem item = ObjectSpace.CreateObject<APInvoiceItem>();
                    item.Amount = inventory.OrderItem.Amount;
                    item.GLAccount = ObjectSpace.GetObjectByKey<GLAccount>(inventory.OrderItem.VendorItem.Item.Inventory.AccountNumber);
                    invoice.Items.Add(item);
                }

                ObjectSpace.CommitChanges();
                invCount += 1;
            }

            Application.ShowViewStrategy.ShowMessage(string.Format("{0} invoice was created, please check AP Invoice to adjust the invoices and post the invoices to Journal Vouchers", invCount), InformationType.Info);
        }

        private void AddAllOutstandingInvoiceAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            APPayment payment = (APPayment)View.CurrentObject;
            //IObjectSpace os = Application.CreateObjectSpace();
            IList invoices = ObjectSpace.GetObjects(typeof(APInvoice), CriteriaOperator.Parse("Vendor.Oid=? and Owing>0", payment.Vendor.Oid, true));
            foreach (APInvoice invoice in invoices)
            {
                APPaymentItem item = ObjectSpace.CreateObject<APPaymentItem>();
                item.Invoice = invoice; ;
                item.Payment = invoice.Owing;

                payment.Items.Add(item);
            }
        }

        private void PostOrderReceipt_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            foreach (InventoryReceipt receipt in View.SelectedObjects)
            {
                receipt.Posted = true;
            }

            ObjectSpace.CommitChanges();
        }

        private void PostAPInvoiceAcction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            int invCount = 0;
            foreach (APInvoice invoice in View.SelectedObjects)
            {
                invCount += 1;
                SystemSetting setting = ObjectSpace.FindObject<SystemSetting>(null);

                JournalVoucher journalVoucher = ObjectSpace.CreateObject<JournalVoucher>();
                journalVoucher.VoucherDate = DateTime.Now;
                journalVoucher.PeriodMonth = invoice.PeriodMonth;
                journalVoucher.PeriodYear = invoice.PeriodYear;
                journalVoucher.Description = string.Format("AP Invoice No. {0}", invoice.InvoiceNumber);

                JournalEntry entryAP = ObjectSpace.CreateObject<JournalEntry>();
                entryAP.Account = ObjectSpace.GetObjectByKey<GLAccount>(invoice.PurchaseOrder.Vendor.Currency.AccountsPayable.AccountNumber);
                entryAP.Amount = invoice.PurchaseOrder.Amount * -1;
                journalVoucher.Entries.Add(entryAP);

                decimal TotalAmount = 0;
                foreach (APInvoiceItem item in invoice.Items)
                {
                    JournalEntry entry = ObjectSpace.CreateObject<JournalEntry>();
                    entry.Account = ObjectSpace.GetObjectByKey<GLAccount>(item.GLAccount.AccountNumber);
                    entry.Amount = item.Amount * invoice.Vendor.Currency.ExchangeRate;
                    TotalAmount += entry.Amount;

                    journalVoucher.Entries.Add(entry);
                }

                if (setting.DefaultCurrency != invoice.Vendor.Currency)
                {
                    JournalEntry entry = ObjectSpace.CreateObject<JournalEntry>();
                    entry.Account = ObjectSpace.GetObjectByKey<GLAccount>(invoice.Vendor.Currency.GainLossOnExchange.AccountNumber);
                    entry.Amount = ((invoice.Amount * invoice.Vendor.Currency.ExchangeRate) - invoice.Amount) * -1;

                    journalVoucher.Entries.Add(entry);
                }

                invoice.Posted = true;

            }

            ObjectSpace.CommitChanges();

            Application.ShowViewStrategy.ShowMessage(string.Format("{0} invoice was created, please check AP Invoice to adjust the invoices and post the invoices to Journal Vouchers", invCount), InformationType.Success, 5000, InformationPosition.Bottom);
        }
    }
}
