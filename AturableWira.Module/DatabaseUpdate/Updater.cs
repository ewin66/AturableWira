using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using AturableWira.Module.BusinessObjects.HRM;
using AturableWira.Module.BusinessObjects.SYS;
using System.IO;
using System.Xml;
using AturableWira.Module.BusinessObjects.ACC.GL;
using static AturableWira.Module.BusinessObjects.ETC.Enums;
using AturableWira.Module.BusinessObjects.ACC;
using AturableWira.Module.BusinessObjects.ERP;
using AturableWira.Module.BusinessObjects.CRM;
using AturableWira.Module.BusinessObjects.ERP.Inventory;
using AturableWira.Module.BusinessObjects.ERP.Purchase;

namespace AturableWira.Module.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
            //string name = "MyName";
            //DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<DomainObject1>();
            //    theObject.Name = name;
            //}
            Employee sampleUser = ObjectSpace.FindObject<Employee>(new BinaryOperator("UserName", "User"));
            if (sampleUser == null)
            {
                sampleUser = ObjectSpace.CreateObject<Employee>();
                sampleUser.UserName = "User";
                sampleUser.FirstName = "Sample";
                sampleUser.LastName = "User";
                sampleUser.SetPassword("");
            }
            EmployeeRole defaultRole = CreateDefaultRole();
            sampleUser.EmployeeRoles.Add(defaultRole);

            Employee userAdmin = ObjectSpace.FindObject<Employee>(new BinaryOperator("UserName", "Admin"));
            if (userAdmin == null)
            {
                userAdmin = ObjectSpace.CreateObject<Employee>();
                userAdmin.UserName = "Admin";
                userAdmin.FirstName = "Super";
                userAdmin.LastName = "Admin";
                // Set a password if the standard authentication type is used
                userAdmin.SetPassword("");
            }
            // If a role with the Administrators name doesn't exist in the database, create this role
            EmployeeRole adminRole = ObjectSpace.FindObject<EmployeeRole>(new BinaryOperator("Name", "Administrators"));
            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<EmployeeRole>();
                adminRole.Name = "Administrators";
            }
            adminRole.IsAdministrative = true;
            userAdmin.EmployeeRoles.Add(adminRole);
            ObjectSpace.CommitChanges(); //This line persists created object(s).


            XmlDocument glDocument = new XmlDocument();
            glDocument.LoadXml(GetInitialData("GLAccount.xml"));
            XmlElement xelRoot = glDocument.DocumentElement;
            XmlNodeList xnlNodes = xelRoot.SelectNodes("/data-set/record");

            foreach (XmlNode xndNode in xnlNodes)
            {
                GLAccount glAccount = ObjectSpace.FindObject<GLAccount>(new BinaryOperator("AccountNumber", xndNode["Account"].InnerText));
                if (glAccount == null)
                {
                    glAccount = ObjectSpace.CreateObject<GLAccount>();
                    glAccount.AccountNumber = xndNode["Account"].InnerText;
                    glAccount.AccountName = xndNode["Name"].InnerText;
                    glAccount.AccountType = (GLACcountType)Convert.ToInt16(xndNode["Type"].InnerText);
                    glAccount.RetainedEarnings = Convert.ToBoolean(xndNode["RetainedEarnings"].InnerText);

                    glAccount.Save();
                }
            }

            glDocument.LoadXml(GetInitialData("Currency.xml"));
            xelRoot = glDocument.DocumentElement;
            xnlNodes = xelRoot.SelectNodes("/data-set/record");
            foreach (XmlNode xndNode in xnlNodes)
            {
                Currency currency = ObjectSpace.FindObject<Currency>(new BinaryOperator("CurrencyCode", xndNode["CurrencyCode"].InnerText));
                if (currency == null)
                {
                    currency = ObjectSpace.CreateObject<Currency>();
                    currency.CurrencyCode = xndNode["CurrencyCode"].InnerText;
                    currency.Name = xndNode["Name"].InnerText;
                    currency.ExchangeRate = Convert.ToDecimal(xndNode["ExchangeRate"].InnerText);
                    currency.AccountsPayable = ObjectSpace.FindObject<GLAccount>(new BinaryOperator("AccountNumber", xndNode["AccountsPayable"].InnerText));
                    currency.APDiscounts = ObjectSpace.FindObject<GLAccount>(new BinaryOperator("AccountNumber", xndNode["APDiscounts"].InnerText));
                    currency.AccountsReceivable = ObjectSpace.FindObject<GLAccount>(new BinaryOperator("AccountNumber", xndNode["AccountsReceivable"].InnerText));
                    currency.ARDiscounts = ObjectSpace.FindObject<GLAccount>(new BinaryOperator("AccountNumber", xndNode["ARDiscounts"].InnerText));
                    currency.DefaultARWriteOffs = ObjectSpace.FindObject<GLAccount>(new BinaryOperator("AccountNumber", xndNode["DefaultARWriteOffs"].InnerText));
                    currency.GainLossOnExchange = ObjectSpace.FindObject<GLAccount>(new BinaryOperator("AccountNumber", xndNode["GainLossOnExchange"].InnerText));

                    currency.Save();
                }
            }

            glDocument.LoadXml(GetInitialData("Bank.xml"));
            xelRoot = glDocument.DocumentElement;
            xnlNodes = xelRoot.SelectNodes("/data-set/record");
            foreach (XmlNode xndNode in xnlNodes)
            {
                Bank bank = ObjectSpace.FindObject<Bank>(new BinaryOperator("Name", xndNode["Name"].InnerText));
                if (bank == null)
                {
                    bank = ObjectSpace.CreateObject<Bank>();
                    bank.Name = xndNode["Name"].InnerText;
                    bank.GLAccount = ObjectSpace.FindObject<GLAccount>(new BinaryOperator("AccountNumber", xndNode["GLAccount"].InnerText));
                    bank.Currency = ObjectSpace.FindObject<Currency>(new BinaryOperator("CurrencyCode", xndNode["CurrencyCode"].InnerText));

                    bank.Save();
                }
            }

            glDocument.LoadXml(GetInitialData("Warehouse.xml"));
            xelRoot = glDocument.DocumentElement;
            xnlNodes = xelRoot.SelectNodes("/data-set/record");
            foreach (XmlNode xndNode in xnlNodes)
            {
                Warehouse warehouse = ObjectSpace.FindObject<Warehouse>(new BinaryOperator("Name", xndNode["Name"].InnerText));
                if (warehouse == null)
                {
                    warehouse = ObjectSpace.CreateObject<Warehouse>();
                    warehouse.Name = xndNode["Name"].InnerText;

                    warehouse.Save();
                }
            }

            glDocument.LoadXml(GetInitialData("ItemType.xml"));
            xelRoot = glDocument.DocumentElement;
            xnlNodes = xelRoot.SelectNodes("/data-set/record");
            foreach (XmlNode xndNode in xnlNodes)
            {
                ItemType itemType = ObjectSpace.FindObject<ItemType>(new BinaryOperator("Name", xndNode["Name"].InnerText));
                if (itemType == null)
                {
                    itemType = ObjectSpace.CreateObject<ItemType>();
                    itemType.Name = xndNode["Name"].InnerText;
                    itemType.Inventory = Convert.ToBoolean(xndNode["Inventory"].InnerText);
                    itemType.CostingMethod = (CostingMethod)Convert.ToInt16(xndNode["CostingMethod"].InnerText);

                    itemType.Save();
                }
            }

            if (ObjectSpace.GetObjectsCount(typeof(SystemSetting), null) == 0)
            {
                SystemSetting systemSetting = ObjectSpace.CreateObject<SystemSetting>();
                systemSetting.CompanyName = "Aturable Solution";
                systemSetting.SalesNumber = 1001;
                systemSetting.PurchaseOrderNumber = 1001;
                systemSetting.InvoiceNumber = 1001;
                systemSetting.InventoryReceiptNumber = 1001;
                systemSetting.Sales = ObjectSpace.FindObject<GLAccount>(new BinaryOperator("AccountNumber", "4000"));
                systemSetting.CostofGoodsSold = ObjectSpace.FindObject<GLAccount>(new BinaryOperator("AccountNumber", "5000"));
                systemSetting.Inventory = ObjectSpace.FindObject<GLAccount>(new BinaryOperator("AccountNumber", "1200"));
                systemSetting.DefaultCurrency = ObjectSpace.FindObject<Currency>(new BinaryOperator("CurrencyCode", "IDR"));

                systemSetting.Save();
                ObjectSpace.CommitChanges();
            }

            glDocument.LoadXml(GetInitialData("Vendor.xml"));
            xelRoot = glDocument.DocumentElement;
            xnlNodes = xelRoot.SelectNodes("/data-set/record");
            foreach (XmlNode xndNode in xnlNodes)
            {
                Vendor vendor = ObjectSpace.FindObject<Vendor>(new BinaryOperator("Name", xndNode["Name"].InnerText));
                if (vendor == null)
                {
                    vendor = ObjectSpace.CreateObject<Vendor>();
                    vendor.Name = xndNode["Name"].InnerText;
                    vendor.DiscountPercent = Convert.ToDecimal(xndNode["DiscountPercent"].InnerText);
                    vendor.DiscountDays = Convert.ToInt16(xndNode["DiscountDays"].InnerText);
                    vendor.Currency = ObjectSpace.GetObjectByKey<Currency>(xndNode["Currency"].InnerText);
                    vendor.DefaultDueDays = Convert.ToInt16(xndNode["DefaultDueDays"].InnerText);
                    vendor.DefaultDescription = xndNode["DefaultDescription"].InnerText;

                    vendor.Save();
                }
            }

            glDocument.LoadXml(GetInitialData("Items.xml"));
            xelRoot = glDocument.DocumentElement;
            xnlNodes = xelRoot.SelectNodes("/data-set/record");
            foreach (XmlNode xndNode in xnlNodes)
            {
                Item item = ObjectSpace.FindObject<Item>(new BinaryOperator("ItemNumber", xndNode["ItemNumber"].InnerText));
                if (item == null)
                {
                    item = ObjectSpace.CreateObject<Item>();
                    item.Name = xndNode["Name"].InnerText;
                    item.ItemNumber = xndNode["ItemNumber"].InnerText;
                    item.Sales = ObjectSpace.FindObject<GLAccount>(new BinaryOperator("AccountNumber", xndNode["Sales"].InnerText));
                    item.CostofGoodsSold = ObjectSpace.FindObject<GLAccount>(new BinaryOperator("AccountNumber", xndNode["CostofGoodsSold"].InnerText));
                    item.Inventory = ObjectSpace.FindObject<GLAccount>(new BinaryOperator("AccountNumber", xndNode["Inventory"].InnerText));
                    item.Sold = Convert.ToBoolean(xndNode["Sold"].InnerText);
                    item.Purchased = Convert.ToBoolean(xndNode["Purchased"].InnerText);
                    item.Type = ObjectSpace.FindObject<ItemType>(new BinaryOperator("Name", xndNode["Type"].InnerText));
                    item.Save();
                }
            }

            glDocument.LoadXml(GetInitialData("VendorItem.xml"));
            xelRoot = glDocument.DocumentElement;
            xnlNodes = xelRoot.SelectNodes("/data-set/record");
            foreach (XmlNode xndNode in xnlNodes)
            {
                VendorItem item = ObjectSpace.FindObject<VendorItem>(new BinaryOperator("Description", xndNode["Description"].InnerText));
                if (item == null)
                {
                    item = ObjectSpace.CreateObject<VendorItem>();
                    item.Item = ObjectSpace.GetObjectByKey<Item>(xndNode["ItemNumber"].InnerText);
                    item.Vendor = ObjectSpace.FindObject<Vendor>(CriteriaOperator.Parse("Name=?", xndNode["Vendor"].InnerText));
                    item.VendorItemNumber = xndNode["VendorItemNumber"].InnerText;
                    item.VendorItemQuantity = Convert.ToInt16(xndNode["VendorItemQuantity"].InnerText);
                    item.Description = xndNode["Description"].InnerText;
                    item.Cost = Convert.ToDecimal(xndNode["Cost"].InnerText);

                    item.Save();
                }
            }

            ObjectSpace.CommitChanges();
        }
        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
        private EmployeeRole CreateDefaultRole()
        {
            EmployeeRole defaultRole = ObjectSpace.FindObject<EmployeeRole>(new BinaryOperator("Name", "Default"));
            if (defaultRole == null)
            {
                defaultRole = ObjectSpace.CreateObject<EmployeeRole>();
                defaultRole.Name = "Default";

                defaultRole.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.Read, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
                defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);
            }
            return defaultRole;
        }

        public string GetInitialData(string filename)
        {
            string result = string.Empty;

            using (Stream stream = this.GetType().Assembly.
                       GetManifestResourceStream("AturableWira.Module.DatabaseUpdate.InitialData." + filename))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }

    }
}
