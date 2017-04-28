using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace AturableWira.Module.BusinessObjects.ETC
{
   [DomainComponent]
   [ModelDefault("Caption", "Add Cost by Percentage")]
   public class AddjustPriceParameterObject
   {
      public static Type AdjustPriceParameterType = typeof(AddjustPriceParameterObject);
      public static AddjustPriceParameterObject CreateShortDescriptionParametersObject()
      {
         return (AddjustPriceParameterObject)ReflectionHelper.CreateObject(AdjustPriceParameterType);
      }
      [ModelDefault("EditMask", "n2")]
      [ModelDefault("DisplayFormat", "{0:n2}%")]
      public decimal AdjustPriceBy { get; set; }
   }
}