using System.Web.Optimization;

namespace MyProject.Web
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
				"~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));
			bundles.Add(new StyleBundle("~/bundles/font-awesome/css").Include(
				"~/Content/font-awesome.min.css", new CssRewriteUrlTransform()));
			bundles.Add(new StyleBundle("~/bundles/custom/css").Include(
				"~/Content/css.css", new CssRewriteUrlTransform()));


			bundles.Add(new ScriptBundle("~/bundles/jquery/js").Include(
				"~/Scripts/jquery-3.3.1.min.js"));
			bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include(
				"~/Scripts/bootstrap.min.js"));
			bundles.Add(new ScriptBundle("~/bundles/unobtrusive/js").Include(
				"~/Scripts/jquery.unobtrusive-ajax.min.js"));
			bundles.Add(new ScriptBundle("~/bundles/validate/js").Include(
				"~/Scripts/jquery.validate.unobtrusive.min.js"));
		}
	}
}