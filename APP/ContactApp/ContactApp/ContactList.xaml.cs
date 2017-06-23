using ContactApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace ContactApp
{
    public partial class ContactList : ContentPage
    {
        public ContactList()
        {
            InitializeComponent();


            this.ContactListLayout.Children.Add(CreateContactElement(new Contact
            {
                FirstName = "Michel",
                LastName = "Durand",
                PhoneNumber = "06 12 34 56 78"
            }));
            this.ContactListLayout.Children.Add(CreateContactElement(new Contact
            {
                FirstName = "Martine",
                LastName = "Dupond",
                PhoneNumber = "06 12 34 56 78"
            }));
            this.ContactListLayout.Children.Add(CreateContactElement(new Contact
            {
                FirstName = "Rémi",
                LastName = "Sansfamille",
                PhoneNumber = "06 12 34 56 78"
            }));
        }

        public View CreateContactElement(Contact contact)
        {
            var contactElement = new StackLayout { Orientation = StackOrientation.Horizontal };
            var elem = new StackLayout { Orientation = StackOrientation.Vertical };
            elem.Children.Add(new Label { Text = contact.FirstName + " " + contact.LastName, VerticalOptions = LayoutOptions.Fill });
            elem.Children.Add(new Label { Text = contact.PhoneNumber, VerticalOptions = LayoutOptions.Fill });
            contactElement.Children.Add(elem);

            var buttonSMS = new Button { Text = "SMS", HorizontalOptions = LayoutOptions.Fill };
            buttonSMS.Clicked += SMS_Clicked(contact.PhoneNumber);
            contactElement.Children.Add(buttonSMS);
            var buttonTel = new Button { Text = "TEL", HorizontalOptions = LayoutOptions.Fill};
            contactElement.Children.Add(buttonTel);

            return contactElement;
        }

        private EventHandler SMS_Clicked(string PhoneNumber)
        {
            var SMS = DependencyService.Get<ICellPhone>();
            SMS.openSMS(PhoneNumber);
            return null;
        }
    }
}
