using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.Msie;

namespace UI.AngularJS
{
    public class JsEngineSwitcherConfig
    {
        public static void Configure(JsEngineSwitcher engineSwitcher)
        {
            engineSwitcher.EngineFactories.AddMsie();

            engineSwitcher.DefaultEngineName = MsieJsEngine.EngineName;
        }
    }
}