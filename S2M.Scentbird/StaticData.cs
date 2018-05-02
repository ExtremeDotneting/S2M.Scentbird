using System.IO;

namespace FakeCasino
{
    static class StaticData
    {
        public static string PolyfillJs { get; }

        static StaticData()
        {
            using (StreamReader sr = new StreamReader(Android.App.Application.Context.Assets.Open("static/polyfill.min.js")))
            {
                PolyfillJs = sr.ReadToEnd();
            }
        }
    }
}