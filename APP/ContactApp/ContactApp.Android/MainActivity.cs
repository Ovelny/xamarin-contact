using System;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
using ContactApp.Pages;
using Android.Graphics;
using Android.Media;
using System.IO;
using Java.IO;
using Android.Provider;
using Android.Database;

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
            if (resultCode != Result.Ok)
                return;
            if (requestCode == 1 || requestCode == 2)
            {
                Bitmap imageBitmap = null;
                if (requestCode == 1) // code 1 : SelectImageFromGallery
                {
                    var stream = ContentResolver.OpenInputStream(data.Data);
                    BitmapFactory.Options options = new BitmapFactory.Options { InSampleSize = 8 }; // réduire la taille de l'image
                    imageBitmap = BitmapFactory.DecodeStream(stream, new Rect(), options);
                    int orientation = GetImageOrientationFromUri(data.Data);
                    if(orientation != 0)
                        imageBitmap = rotateImage(imageBitmap, orientation);
                }
                else if (requestCode == 2) // code 2 : TakePicture
                {
                    imageBitmap = (Bitmap)data.Extras.Get("data");
                }
                var stack = app.MainPage.Navigation.NavigationStack;
                var contactDetailPage = stack[stack.Count - 1] as ContactDetail;
                if (contactDetailPage != null)
                    contactDetailPage.UpdatePhoto(imageBitmap);
                else
                    throw new Exception("Page détail contact non trouvée pour l'appel à UpdatePhoto");
                imageBitmap.Dispose();
            }
        }

        /// <summary>
        /// Récupère l'orientation de l'image.
        /// </summary>
        /// <returns>L'orientation en degrés</returns>
        private int GetImageOrientationFromUri(Android.Net.Uri imageUri)
        {
            // Get the id from the uri
            int index = imageUri.Path.LastIndexOf(":"); // paths are like "documents/image:1234"
            string imageId = imageUri.Path.Substring(index+1);

            // Query the image storage for our image
            string[] imageColumns = { MediaStore.Images.Media.InterfaceConsts.Id, MediaStore.Images.ImageColumns.Orientation };
            //string imageOrderBy = MediaStore.Images.Media.InterfaceConsts.Id + " DESC";
            string where = MediaStore.Images.Media.InterfaceConsts.Id + " = ?";
            ICursor cursor = ContentResolver.Query(MediaStore.Images.Media.ExternalContentUri, imageColumns, where, new string[] { imageId }, null);

            if (cursor.MoveToFirst())
            {
                int orientation = cursor.GetInt(cursor.GetColumnIndex(MediaStore.Images.ImageColumns.Orientation));
                cursor.Close();
                return orientation;
            }
            else
            {
                return 0;
            }
        }

        private Bitmap rotateImage(Bitmap sourceBitmap, int degrees)
        {
            Matrix matrix = new Matrix();
            matrix.PostRotate(degrees);
            return Bitmap.CreateBitmap(sourceBitmap, 0, 0, sourceBitmap.Width, sourceBitmap.Height, matrix, true);
        }
    }
}

