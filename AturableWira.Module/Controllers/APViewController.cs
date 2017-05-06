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
using AturableWira.Module.BusinessObjects.ERP;

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
            foreach (InventoryReceipt receipt in View.SelectedObjects)
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
                invoice.Description = String.Format("Receipt: {0} InventoryReceiptItem", receipt.ReceiptNumber);
                receipt.APInvoice = invoice;

                foreach (InventoryReceiptItem inventory in receipt.Items)
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
                item.Invoice = invoice;
                item.Amount = invoice.Owing;
                item.Payment = invoice.Owing;

                payment.Items.Add(item);
            }
        }

        private void PostOrderReceipt_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            foreach (InventoryReceipt receipt in View.SelectedObjects)
            {
                foreach (InventoryReceiptItem item in receipt.Items)
                {
                    Item productItem = ObjectSpace.GetObjectByKey<Item>(item.VendorItem.Item.ItemNumber);
                    productItem.Cost = item.UnitCost;
                }

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
                journalVoucher.Source = BusinessObjects.ETC.Enums.JournalVoucherSource.APInvoice;
                journalVoucher.Description = string.Format("AP Invoice {0} - {1}", invoice.InvoiceNumber, invoice.Vendor.Name);

                JournalEntry entryAP = ObjectSpace.CreateObject<JournalEntry>();
                entryAP.Account = ObjectSpace.GetObjectByKey<GLAccount>(invoice.Vendor.Currency.AccountsPayable.AccountNumber);
                entryAP.Amount = invoice.Amount * -1;
                journalVoucher.Entries.Add(entryAP);
                decimal TotalAmount = 0;
                foreach (APInvoiceItem item in invoice.Items)
                {
                    JournalEntry entry = ObjectSpace.CreateObject<JournalEntry>();
                    entry.Account = ObjectSpace.GetObjectByKey<GLAccount>(item.GLAccount.AccountNumber);
                    if (invoice.Vendor.Currency.ExchangeRate > 0)
                        entry.Amount = item.Amount * invoice.Vendor.Currency.ExchangeRate;
                    else
                        entry.Amount = item.Amount;
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

        private void PostAPPaymentAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            int invCount = 0;
            foreach (APPayment payment in View.SelectedObjects)
            {
                SystemSetting setting = ObjectSpace.FindObject<SystemSetting>(null);

                JournalVoucher journalVoucher = ObjectSpace.CreateObject<JournalVoucher>();
                journalVoucher.VoucherDate = DateTime.Now;
                journalVoucher.PeriodMonth = payment.PeriodMonth;
                journalVoucher.PeriodYear = payment.PeriodYear;
                journalVoucher.Source = BusinessObjects.ETC.Enums.JournalVoucherSource.APPayment;
                journalVoucher.Description = string.Format("AP Payment Ref. {0}", payment.Reference);

                JournalEntry entryAP = ObjectSpace.CreateObject<JournalEntry>();
                entryAP.Account = ObjectSpace.GetObjectByKey<GLAccount>(payment.Bank.GLAccount.AccountNumber);
                entryAP.Amount = payment.Amount * -1;
                journalVoucher.Entries.Add(entryAP);

                foreach (APPaymentItem item in payment.Items)
                {
                    JournalEntry entry = ObjectSpace.CreateObject<JournalEntry>();
                    entry.Account = ObjectSpace.GetObjectByKey<GLAccount>(payment.Vendor.Currency.AccountsPayable.AccountNumber);
                    if (payment.Vendor.Currency.ExchangeRate > 0)
                        entry.Amount = item.Payment;// * payment.Vendor.Currency.ExchangeRate;
                    else
                        entry.Amount = item.Payment;
                    journalVoucher.Entries.Add(entry);
                }
                if (setting.DefaultCurrency != payment.Vendor.Currency)
                {
                    JournalEntry entry = ObjectSpace.CreateObject<JournalEntry>();
                    entry.Account = ObjectSpace.GetObjectByKey<GLAccount>(payment.Vendor.Currency.GainLossOnExchange.AccountNumber);
                    entry.Amount = ((payment.Amount * payment.Vendor.Currency.ExchangeRate) - payment.Amount);

                    journalVoucher.Entries.Add(entry);
                }
                invCount += 1;
                payment.Posted = true;
            }

            ObjectSpace.CommitChanges();
            Application.ShowViewStrategy.ShowMessage(string.Format("{0} payments was posted, please check JV Voucher to adjust the JVs and post the JVs to General Ledger", invCount), InformationType.Success, 5000, InformationPosition.Bottom);
        }
    }
}
