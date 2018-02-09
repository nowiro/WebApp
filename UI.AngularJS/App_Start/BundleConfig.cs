using System.Web.Optimization;
using BundleTransformer.Core.Resolvers;
using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Transformers;

namespace UI.AngularJS
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            bundles.UseCdn = false;

            // Initialize all of the related builders, transformers, orderers etc.
            var nullBuilder = new NullBuilder();
            var styleTransformer = new StyleTransformer();
            var scriptTransformer = new ScriptTransformer();
            var nullOrderer = new NullOrderer();

            // Replace a default bundle resolver in order to the debugging HTTP-handler
            // can use transformations of the corresponding bundle
            BundleResolver.Current = new CustomBundleResolver();

            // Initialize the common styles bundle
            var commonStylesBundle = new Bundle("~/bundles/style");
            commonStylesBundle.Include(
                "~/Content/_ViewStart.less"
            );
            commonStylesBundle.Builder = nullBuilder;
            commonStylesBundle.Transforms.Add(styleTransformer);
            // Add the css minifier
            commonStylesBundle.Transforms.Add(new CssMinify());
            commonStylesBundle.Orderer = nullOrderer;
            bundles.Add(commonStylesBundle);

            // Initialize the common scripts bundle
            var commonScriptsBundle = new Bundle("~/bundles/scripts");
            commonScriptsBundle.Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.js",
                // AngularJS versions https://code.angularjs.org/
                "~/Scripts/AngularJS/angular.js",
                "~/Scripts/AngularJS/angular-route.js",


                "~/Scripts/_ViewStart.js"
            );

            commonScriptsBundle.Builder = nullBuilder;
            commonScriptsBundle.Transforms.Add(scriptTransformer);
            commonScriptsBundle.Orderer = nullOrderer;
            bundles.Add(commonScriptsBundle);

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            // Initialize the common scripts bundle
            var modernizrScriptsBundle = new Bundle("~/bundles/modernizr");
            modernizrScriptsBundle.Include(
                "~/Scripts/modernizr-*");
            modernizrScriptsBundle.Builder = nullBuilder;
            modernizrScriptsBundle.Transforms.Add(scriptTransformer);
            modernizrScriptsBundle.Orderer = nullOrderer;
            bundles.Add(modernizrScriptsBundle);

            // Initialize the home view scripts bundle
            var homeViewScriptsBundle = new Bundle("~/bundles/scriptsViewHome");
            homeViewScriptsBundle.Include(
                "~/Scripts/Views/Home/Config/AppConfig.js",
                "~/Scripts/Views/Home/Module/App.js",
                "~/Scripts/Views/Home/Controller/HomeController.js",
                "~/Scripts/Views/Home/Helper/DefaultHelper.js",
                "~/Scripts/Views/Home/Service/DefaultService.js"
            );

            homeViewScriptsBundle.Builder = nullBuilder;
            homeViewScriptsBundle.Transforms.Add(scriptTransformer);
            homeViewScriptsBundle.Orderer = nullOrderer;
            bundles.Add(homeViewScriptsBundle);

            //BundleTable.EnableOptimizations = true; //Inherited from Web.config debug status <compilation debug="false" targetFramework="4.5" />
            //BundleTable.EnableOptimizations = false;
        }
    }
}