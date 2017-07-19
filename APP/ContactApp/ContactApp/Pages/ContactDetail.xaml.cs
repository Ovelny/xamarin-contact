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
        private ContactList ParentPage { get; set; }
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

        public ContactDetail(ContactList parent, int idContact=-1)
        {
            this.ParentPage = parent;
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

        private async void BtnEnregistrer(object sender, EventArgs e)
        {
            if (contact.Photos == null)
                contact.Photos = "";
            try
            {
                if (this.contact.Id == -1)
                {
                    var newId = await this.repositoryContact.addContact(this.contact);
                    contact.Id = newId;
                }
                else
                    await this.repositoryContact.editContact(contact.Id, contact);
                DisplayAlert("", "Le contact a été sauvegardé", "Ok");
                FireContactSavedEvent();
            }
            catch (Exception ex)
            {
                DisplayAlert("", "Une erreur est survenue", "Ok");
            }
        }

        public void FireContactSavedEvent()
        {
            this.ParentPage.OnContactSaved();
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
            if (bitmap == null)
            {
                contact.Photos = "";
                photo = null;
                return;
            }
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