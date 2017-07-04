using ContactApp.DataHandlers;
using ContactApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactDetail : ContentPage
    {
        private IRepository repositoryContact;
        public Contact contact { get; set; }

        public ImageSource photo { get; set; }

        public bool displayPlaceholder
        {
            get
            {
                return contact.Photo == null || contact.Photo == "";
            }
        }

        public ContactDetail(int idContact=-1)
        {
            this.repositoryContact = new ContactRedoLog();

            if (idContact != -1)
                this.contact = repositoryContact.getContact(idContact);
            else
                this.contact = new Contact { Id = -1 };

            if (contact.Photo != null && contact.Photo != "")
            {
                Byte[] base64 = System.Convert.FromBase64String(contact.Photo);
                photo = ImageSource.FromStream(() => new MemoryStream(base64));
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
    }
}