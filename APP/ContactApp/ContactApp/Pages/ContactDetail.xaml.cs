using ContactApp.DataHandlers;
using ContactApp.Models;
using System;
using System.Collections.Generic;
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

        public ContactDetail(int idContact=-1)
        {

            this.repositoryContact = new ContactRedoLog();

            if (idContact != -1)
                this.contact = repositoryContact.getContact(idContact);
            else
                this.contact = new Contact();

            InitializeComponent();
        }
    }
}