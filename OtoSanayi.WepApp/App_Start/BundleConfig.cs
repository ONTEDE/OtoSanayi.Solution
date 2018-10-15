using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace OtoSanayi.WepApp
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles) {

            //CSS Style Bundles
            bundles.Add(new StyleBundle("~/Content/all").
                Include("~/Content/plugins/footable/footable.core.css").
                Include("~/Content/css/animate.css").
                Include("~/Content/css/bootstrap.min.css").
                Include("~/Content/font-awesome.min.css").
                Include("~/Content/magnific-popup.css").
                Include("~/Content/slick.css").
                Include("~/Content/jplayer.css").
                Include("~/Content/main.css").
                Include("~/Content/responsive.css"));


            //Js Script Bundles
            bundles.Add(new ScriptBundle("~/js/all").
                Include("~/scripts/js/jquery.min.js").
                Include("~/scripts/bootstrap.min.js").
                Include("~/scripts/js/marquee.js").
                Include("~/scripts/js/moment.min.js").
                Include("~/scripts/js/theia-sticky-sidebar.js").
                Include("~/scripts/js/jquery.jplayer.min.js").
                Include("~/scripts/js/jplayer.playlist.min.js").
                Include("~/scripts/js/slick.min.js").
                Include("~/scripts/js/carouFredSel.js").
                Include("~/scripts/js/jquery.magnific-popup.js").
                Include("~/scripts/js/main.js").
                Include("~/scripts/Admin/plugins/footable/footable.all.min.js"));

            BundleTable.EnableOptimizations = true;

        }
    }
}