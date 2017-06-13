using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
namespace Dormitary_Re.App_Start
{
    public class BundleConfig 
    {
        public static void RegisterBundles(BundleCollection Bundles)
        {
            //Bundles.Add(new ScriptBundle("~/DormitaryJS").Include(
            //    "~/DormJS/"
            //    ));
            Bundles.Add(new StyleBundle("~/DormitaryCSS/css").Include(
                "~/Content/Dormitary.css"
                ));
        }
    }
}