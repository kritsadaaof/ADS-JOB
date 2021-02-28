using System.Web;
using System.Web.Optimization;

namespace JOB_ADS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/PostJob/js").Include(
                      "~/Scripts/PostJob/js_Postjob.js"));

            bundles.Add(new ScriptBundle("~/AccountLogin/js").Include(
                      "~/Scripts/Account/js_Login.js"));

            bundles.Add(new ScriptBundle("~/AccountEditJOB/js").Include(
                      "~/Scripts/Account/js_EditJOB.js"));

            bundles.Add(new ScriptBundle("~/HomeIndex/js").Include(
                      "~/Scripts/Home/js_Index.js"));

            bundles.Add(new ScriptBundle("~/HomeJobList/js").Include(
                      "~/Scripts/Home/js_JobList.js"));

            bundles.Add(new ScriptBundle("~/RegisterForm/js").Include(
                      "~/Scripts/Home/js_RegisterForm.js"));

            bundles.Add(new ScriptBundle("~/ManageCandidates/js").Include(
                      "~/Scripts/ManageCandidates/js_ManageCandidates.js"));

            bundles.Add(new ScriptBundle("~/FindByCandidates/js").Include(
                      "~/Scripts/ManageCandidates/js_FindByCandidates.js"));

            bundles.Add(new ScriptBundle("~/CandidatesDetail/js").Include(
                      "~/Scripts/ManageCandidates/js_CandidatesDetail.js"));

            bundles.Add(new ScriptBundle("~/FindByDepart/js").Include(
                      "~/Scripts/ManageCandidates/js_FindByDepart.js"));
           
            bundles.Add(new ScriptBundle("~/Interview/js").Include(
                      "~/Scripts/ManageCandidates/js_Interview.js"));
        }
    }
}
