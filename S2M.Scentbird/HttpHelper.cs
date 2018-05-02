using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FakeCasino
{
    static class HttpHelper
    {
        public static string TrySendHttpRequest(string url)
        {
            string res = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.Method = "GET";
                request.Timeout = 10000;

                WebResponse response = request.GetResponse();

                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    res = stream.ReadToEnd();

                }
                response.Dispose();
            }
            catch
            {

            }
            return res;
        }
    }
}