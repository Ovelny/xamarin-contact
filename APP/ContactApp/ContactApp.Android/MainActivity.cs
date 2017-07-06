using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using ContactApp.Pages;

namespace ContactApp.Droid
{
    [Activity(Label = "ContactApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected App app;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            app = new App();
            LoadApplication(app);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (resultCode == Result.Ok && requestCode == 1) // code 1 : SelectImageFromGallery
            {
                var stream = ContentResolver.OpenInputStream(data.Data); 
                var stack = app.MainPage.Navigation.NavigationStack; 
                var contactDetailPage = stack[stack.Count - 1] as ContactDetail;
                if (contactDetailPage != null)
                    contactDetailPage.ImageSelected(stream);
                else
                    throw new Exception("Page détail contact non trouvée pour l'appel à ImageSelected");
            }
        }
    }
}

