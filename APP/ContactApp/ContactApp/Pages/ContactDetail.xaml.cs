using System;
using System.ComponentModel;
using System.IO;
using Android.Graphics;
using ContactApp.DataHandlers;
using ContactApp.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactDetail : ContentPage
    {
        #region members
        private IRepository repositoryContact;
        public Contact contact { get; set; }

        public ImageSource photo { get; set; }

        public bool contactPhotoIsDefined
        {
            get
            {
                return contact.Photo != null && contact.Photo != "";
            }
        }
        #endregion

        public ContactDetail(int idContact=-1)
        {
            this.BindingContext = this;
            this.repositoryContact = new ContactRedoLog();

            if (idContact != -1)
                this.contact = repositoryContact.getContact(idContact);
            else
                this.contact = new Contact { Id = -1 };
            
            UpdateDisplayedPhoto();
        }

        public void UpdateDisplayedPhoto()
        {
            if (contactPhotoIsDefined)
            {
                Byte[] base64 = System.Convert.FromBase64String(contact.Photo);
                photo = ImageSource.FromStream(() => new MemoryStream(base64));
            }
            else
            {
                photo = ImageSource.FromFile("placeholder.png");
            }
            InitializeComponent();
        }

        // NB: ne marche pas vraiment, parce qu'on a des données mockées. 
        // Ce code n'aura pas besoin d'être modifié lors du passage aux vraies données.
        private void BtnEnregistrer(object sender, EventArgs e)
        {
            if (this.contact.Id == -1)
                this.repositoryContact.addContact(this.contact);
            else
                this.repositoryContact.editContact(contact.Id, contact);
            DisplayAlert("", "Le contact a été sauvegardé", "Ok");
        }

        public async void ImageTapped(object sender, EventArgs e)
        {
            var sel = "Sélectionner depuis la galerie";
            var del = contactPhotoIsDefined ? "Supprimer l'image du contact" : null;
            var action = await DisplayActionSheet("Modifier la photo du contact", "Annuler", del, sel);
            if (action == sel)
            {
                DependencyService.Get<ICellPhone>().SelectImageFromGallery();
            }
            else if (action == del)
            {
                contact.Photo = null;
                UpdateDisplayedPhoto();
            }
        }

        public void ImageSelected(Stream stream)
        {
            byte[] bitmapData = GetImageByteArrayFromStream(stream);
            contact.Photo = System.Convert.ToBase64String(bitmapData);
            photo = ImageSource.FromStream(() => new MemoryStream(bitmapData));
            InitializeComponent();
        }

        public byte[] GetImageByteArrayFromStream(Stream stream)
        {
            Bitmap bm = BitmapFactory.DecodeStream(stream);

            byte[] bitmapData;
            using (var st = new MemoryStream())
            {
                bm.Compress(Bitmap.CompressFormat.Png, 0, st);
                bitmapData = st.ToArray();
            }
            return bitmapData;
        }
    }
}