using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Java.Interop;

namespace FakeCasino
{
    class MyJSInterface : Java.Lang.Object
    {

        //public event Action<Tuple<string , string >> ReceivedValueFromJs;

        Context context;

        public MyJSInterface(Context context)
        {
            this.context = context;
        }

        [Export]
        [JavascriptInterface]
        public void SendVarToCSharp(string varName, string varValue)
        {
            //if (ReceivedValueFromJs != null && url.Contains(valueInjectKey))
            //{
            //    int index = url.IndexOf(valueInjectKey) + valueInjectKey.Length;
            //    string allData = url.Substring(index);
            //    string varName = url.Split('=')[0];
            //    string varValue = url.Substring(varName.Length);
            //    ReceivedValueFromJs.Invoke(
            //        (varName, varValue)
            //        );
            //}
            //ReceivedValueFromJs.Invoke(
            //        new Tuple<string, string>(varName, varValue)
            //        );
            LocalStorage.Set(varName, varValue);
        }

        [Export]
        [JavascriptInterface]
        public void ShowMsg(string msg)
        {
            Toast.MakeText(Android.App.Application.Context, msg, ToastLength.Long).Show();
        }
    }
}