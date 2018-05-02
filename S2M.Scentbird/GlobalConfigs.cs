using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FakeCasino
{
    static class GlobalConfigs
    {
        public static string SiteUrl { get; } = "https://www.scentbird.com/";

        public static FacebookSdkStatus FacebookSdkStatus { get; } = FacebookSdkStatus.UseOnAllPages;
        public static bool FacebookSdkEnableCookies { get; } = true;
        public static string FacebookAppId { get; } = "812513332281495";//"427017317744736";
        public static bool IsYandexMetricaEnabled { get; } = true;
        public static string YandexMetricaApiKey { get; } = "6c40543e-b17e-4527-9272-cb31e8f7aa16";

    }
}