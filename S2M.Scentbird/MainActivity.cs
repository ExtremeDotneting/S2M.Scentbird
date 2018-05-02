using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Webkit;
using System.IO;
using System.Net;
using System;
using Java.Lang;
using System.Threading.Tasks;
using Android.Content.PM;
using Android.Content.Res;
using Com.Yandex.Metrica;

namespace FakeCasino
{
    [Activity(Label = "SCENTBIRD", MainLauncher = true, Icon = "@mipmap/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public partial class MainActivity : Activity
    {        
        public bool IsVisible { get; private set; }

        protected override void OnPause()
        {
            base.OnPause();
            IsVisible = false;
        }

        protected override void OnResume()
        {
            base.OnPause();
            IsVisible = true;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            InitActivity();
            WebView wv =FindViewById<WebView>(Resource.Id.MyWebView);
            InitWebView(wv);
            MarketingSdkManager.UseYandexMetrica();
            LoadContentToWebView(wv);

            //StartLicenseCheck();
        }

        void InitWebView(WebView wv)
        {
            wv.Settings.JavaScriptEnabled = true;

#if DEBUG
            WebView.SetWebContentsDebuggingEnabled(true);
#endif

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
            {
                CookieManager.Instance.SetAcceptThirdPartyCookies(wv, true);
            }
            else
            {
                CookieManager.Instance.SetAcceptCookie(true);
            }

            wv.Settings.SetPluginState(WebSettings.PluginState.On);
            wv.Settings.LoadWithOverviewMode=true;
            ///wv.Settings.UseWideViewPort = true;
            wv.Settings.AllowContentAccess = true;
            wv.Settings.DomStorageEnabled = true;
            wv.Settings.MixedContentMode = MixedContentHandling.AlwaysAllow;
            try
            {
                wv.Settings.SafeBrowsingEnabled = false;
            }
            catch { }
            bool bq=wv.IsPrivateBrowsingEnabled;
            wv.Settings.DatabaseEnabled = true;
            InitWebViewCaching(wv);


            wv.SetWebChromeClient(new WebChromeClient());
            var wvc = new MyWebViewClient();
            //add event on PageFinishe, that invoke script of facebook sdk
            MarketingSdkManager.UseFacebookSdk(wvc);

            wv.SetWebViewClient(wvc);
            wv.AddJavascriptInterface(new MyJSInterface(this), "CSharp");
            UseBackButtonCrunch(wv);
        }

        void InitWebViewCaching(WebView wv)
        {
            wv.Settings.CacheMode = CacheModes.Normal;
            wv.Settings.SetAppCacheMaxSize(100 * 1024 * 1024);
            wv.Settings.SetAppCacheEnabled(true);
            try
            {
                string cacheDirectory = Path.Combine(Android.App.Application.Context.GetExternalFilesDir("data").CanonicalPath, "br_cache");
                if (!Directory.Exists(cacheDirectory))
                {
                    Directory.CreateDirectory(cacheDirectory);
                }
                wv.Settings.SetAppCachePath(cacheDirectory);
            }
            catch { }
            
            
        }

        void UseBackButtonCrunch(WebView wv)
        {
            int backTaps = 0;
            var ev = new EventHandler<View.KeyEventArgs>((s, e) =>
              {
                  if (e.KeyCode == Keycode.Back)
                  {
                      e.Handled = true;
                      if (backTaps > 1)
                      {
                          if (wv.CanGoBack())
                          {
                              backTaps = 0;
                              wv.GoBack();

                          }
                          else
                          {
                              Finish();
                          }
                      }
                      else
                      {
                          backTaps++;
                      }
                  }

              });
            wv.KeyPress += ev;
        }

        void LoadContentToWebView(WebView wv)
        {
            wv.Visibility = ViewStates.Invisible;
            wv.LoadUrl(GlobalConfigs.SiteUrl);
            bool isConnectedToInterner = HttpHelper.TrySendHttpRequest("https://google.com")?.Length>100;
            if (!isConnectedToInterner)
            {
                wv.StopLoading();
                wv.LoadUrl("file:///android_asset/static/error.html");
            }
            wv.Visibility = ViewStates.Visible;
        }

        void InitActivity()
        {

            RequestWindowFeature(WindowFeatures.NoTitle);
            Window.AddFlags(WindowManagerFlags.Fullscreen);

            SetContentView(Resource.Layout.Main);

        }

        void StartLicenseCheck()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    if (IsVisible)
                    {
                        RunOnUiThread(() =>
                        {
                            Toast.MakeText(Android.App.Application.Context, "Эта тестовая версия, не предназначенна ля публикации.", ToastLength.Long).Show();
                        });
                    }

                    await Task.Delay(3000);
                    
                }
           });
        }

    }
}

