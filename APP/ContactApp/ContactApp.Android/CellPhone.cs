using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Xamarin.Forms;
using ContactApp.Droid;
using Android.Provider;
using Android.Content.PM;

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
            ((Activity)Forms.Context).StartActivityForResult(Intent.CreateChooser(imageIntent, "Choisir une photo"), 1);
        }

        public void TakePicture()
        {
            Intent takePictureIntent = new Intent(MediaStore.ActionImageCapture);
            // on vérifie qu'il y a une app permettant de prendre des photos
            if (takePictureIntent.ResolveActivity(Forms.Context.PackageManager) != null)
            {
                ((Activity)Forms.Context).StartActivityForResult(takePictureIntent, 2);
            }
        }
    }
}