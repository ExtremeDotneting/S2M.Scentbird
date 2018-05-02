using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace FakeCasino
{
    public class MyWebViewClient : WebViewClient
    {
       
        public event Action<WebView, string> PageFinishedEvent;
        public event Action<WebView, string> PageFinishedEventWithTimeout;

        DateTime lastPageLoad = DateTime.MinValue;
        
        [Obsolete]
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {           
            view.LoadUrl(url);
            return false;
        }

        public override void OnPageFinished(WebView view, string url)
        {
            base.OnPageFinished(view, url);
            

            //Костыль, ставит таймаут (10 секунд) на загрузку fb sdk, чтоб предотвратить рекурсивную загрузку.
            if (DateTime.Now- lastPageLoad > new TimeSpan(0,0,0,10))
            {
                lastPageLoad = DateTime.Now;
                
                view.EvaluateJavascript(StaticData.PolyfillJs, null);
                PageFinishedEventWithTimeout?.Invoke(view, url);
            }

            PageFinishedEvent?.Invoke(view, url);

        }
    }
}