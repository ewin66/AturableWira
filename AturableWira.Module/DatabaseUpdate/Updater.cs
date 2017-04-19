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



            if (ObjectSpace.GetObjectsCount(typeof(SystemSetting), null) == 0)
            {
                SystemSetting systemSetting = ObjectSpace.CreateObject<SystemSetting>();
                systemSetting.CompanyName = "Aturable Solution";
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
