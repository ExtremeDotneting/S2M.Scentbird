using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FakeCasino
{
    static class LocalStorage
    {
        public static void Set(string key, string value)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            prefs.Edit().PutString(key, value).Commit();
        }

        public static string Get(string key)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            return prefs.GetString(key, null);
        }
    }
}