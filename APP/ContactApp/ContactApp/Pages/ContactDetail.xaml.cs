using System;
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
                return contact != null && contact.Photos != null && contact.Photos != "";
            }
        }
        #endregion

        public ContactDetail(int idContact=-1)
        {
            this.BindingContext = this;
            this.repositoryContact = new ContactRedoLog();
            InitContact(idContact);
        }

        public async void InitContact(int idContact)
        {
            contact = null;
            if (idContact != -1)
                contact = await repositoryContact.getContact(idContact);
            if (contact == null)
                contact = new Contact { Id = -1 };

            UpdateDisplayedPhoto();
        }

        public void UpdateDisplayedPhoto()
        {
            if (contactPhotoIsDefined)
            {
                Byte[] base64 = System.Convert.FromBase64String(contact.Photos);
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
            if (contact.Photos == null)
                contact.Photos = "";
            try
            {
                if (this.contact.Id == -1)
                    this.repositoryContact.addContact(this.contact);
                else
                    this.repositoryContact.editContact(contact.Id, contact);
                DisplayAlert("", "Le contact a été sauvegardé", "Ok");
            }
            catch (Exception ex)
            {
                DisplayAlert("", "Une erreur est survenue", "Ok");
            }
        }

        public async void ContactPhotoTapped(object sender, EventArgs e)
        {
            var optionSelect = "Sélectionner depuis la galerie";
            var optionSuppr = contactPhotoIsDefined ? "Supprimer l'image du contact" : null;
            var optionPhoto = "Prendre une photo";
            var action = await DisplayActionSheet("Modifier la photo du contact", "Annuler", optionSuppr, optionSelect, optionPhoto);
            if (action == optionSelect)
            {
                DependencyService.Get<ICellPhone>().SelectImageFromGallery();
            }
            else if (action == optionSuppr)
            {
                contact.Photos = "";
                UpdateDisplayedPhoto();
            }
            else if (action == optionPhoto)
            {
                DependencyService.Get<ICellPhone>().TakePicture();
            }
        }
        
        public void UpdatePhoto(Bitmap bitmap)
        {
            byte[] bitmapData = GetByteArrayFromBitmap(bitmap);
            contact.Photos = System.Convert.ToBase64String(bitmapData);
            photo = ImageSource.FromStream(() => new MemoryStream(bitmapData));
            InitializeComponent();
        }

        public byte[] GetByteArrayFromBitmap(Bitmap bitmap)
        {
            byte[] bitmapData;
            using (var st = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, st);
                bitmapData = st.ToArray();
            }
            return bitmapData;
        }
    }
}