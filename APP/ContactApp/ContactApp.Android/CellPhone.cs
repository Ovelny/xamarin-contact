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
    public class CellPhone : Activity, ICellPhone
    {
        public void OpenSMS(string PhoneNumber)
        {
            PhoneNumber = PhoneNumber.Replace(" ", "");
            Device.OpenUri(new Uri(String.Format("sms:{0}", PhoneNumber)));
        }

        public void CallContact(string PhoneNumber)
        {
            PhoneNumber = PhoneNumber.Replace(" ", "");
            var intent = new Intent(Intent.ActionDial);
            intent.SetData(Android.Net.Uri.Parse("tel:" + PhoneNumber));
            Forms.Context.StartActivity(intent);
        }

        public void SelectImageFromGallery()
        {
            var imageIntent = new Intent();
            imageIntent.SetType("image/*");
            imageIntent.SetAction(Intent.ActionGetContent);
            ((Activity)Forms.Context).StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), 1);
        }
    }
}