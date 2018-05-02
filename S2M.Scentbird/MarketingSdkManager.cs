using Android.Webkit;
using Com.Yandex.Metrica;

namespace FakeCasino
{
    static class MarketingSdkManager
    {
        public static void UseFacebookSdk(MyWebViewClient wvc)
        {
            wvc.PageFinishedEventWithTimeout += (view, url) =>
            {
                if (GlobalConfigs.FacebookSdkStatus.HasFlag(FacebookSdkStatus.Disabled))
                    return;

                string script = null;
                string cookieEnabled = GlobalConfigs.FacebookSdkEnableCookies ? "!0" : "0";
                string facebookSdkScript = "console.log('start fb sdk'),window.fbAsyncInit=function(){FB.init({appId:'" +
                    GlobalConfigs.FacebookAppId +
                    "',cookie:COOOOKIE" +
                    ",xfbml:!0,version:'v2.10'}),FB.AppEvents.logPageView()},function(e,n,t){var o,s=e.getElementsByTagName(n)[0];e.getElementById(t)||" +
                    "((o=e.createElement(n)).id=t,o.src='https://connect.facebook.net/en_US/sdk.js',s.parentNode.insertBefore(o,s))}" +
                    "(document,'script','facebook-jssdk'),console.log('fb sdk loaded');";

                var jsToCreateIframe = $@"
  console.log('use fb sdk with iframes');
  var iframe = document.createElement('iframe');" +

      $"var html = \"<body><script>{facebookSdkScript.Replace("COOOOKIE", "0")}</script></body>\";" +

      @"
  iframe.src = 'data:text/html;charset=utf-8,' + encodeURI(html);
  var iframeDiv = document.createElement('div');
  
  document.body.appendChild(iframeDiv);
  iframe.height=1;
  iframe.width=1;
  iframeDiv.style.visibility = 'hidden'; 
  iframeDiv.style.display = 'none';  
  iframeDiv.appendChild(iframe);
";
                facebookSdkScript = facebookSdkScript.Replace("COOOOKIE", cookieEnabled);

                if (GlobalConfigs.FacebookSdkStatus.HasFlag(FacebookSdkStatus.UseOnAllPages))
                {
                    script = facebookSdkScript;
                }
                else if (GlobalConfigs.FacebookSdkStatus.HasFlag(FacebookSdkStatus.UseOnLocalPages) && url.Contains("file:///"))
                {
                    script = facebookSdkScript;
                }
                else if (GlobalConfigs.FacebookSdkStatus.HasFlag(FacebookSdkStatus.UseInIFrames))
                {
                    script = jsToCreateIframe;
                }
                else
                {
                    return;
                }

                view.EvaluateJavascript(script, null);
            };
        }

        public static void UseYandexMetrica()
        {
            if (GlobalConfigs.IsYandexMetricaEnabled)
            {
                YandexMetricaAndroid.YandexMetricaImplementation.Activate(
                    Android.App.Application.Context,
                    GlobalConfigs.YandexMetricaApiKey
                    );
                Log("Launch app.");
            }
        }

        public static void Log(string msg)
        {
            try
            {
                YandexMetrica.ReportEvent(msg);
            }
            catch 
            {

            }
        }
    }
}