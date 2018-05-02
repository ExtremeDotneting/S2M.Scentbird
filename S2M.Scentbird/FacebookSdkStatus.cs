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
    [Flags]
    enum FacebookSdkStatus
    {
        Disabled=1,
        UseOnLocalPages=2,
        UseOnAllPages=4,
        UseInIFrames=8

    }
}