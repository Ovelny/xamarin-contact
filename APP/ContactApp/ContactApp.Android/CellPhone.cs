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
using Xamarin.Forms;
using ContactApp.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(CellPhone))]
namespace ContactApp.Droid
{
    public class CellPhone : ICellPhone
    {
        public void openSMS(string PhoneNumber)
        {
            var intent = new Intent(Intent.ActionCall);
            intent.SetData(Android.Net.Uri.Parse("smsto:" + PhoneNumber));
            Forms.Context.StartActivity(intent);
        }
    }
}