using ContactApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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

            var button = new Button { Text = "SMS", HorizontalOptions = LayoutOptions.Fill };
            contactElement.Children.Add(button);
            button = new Button { Text = "TEL", HorizontalOptions = LayoutOptions.Fill };
            contactElement.Children.Add(button);

            return contactElement;
        }
    }
}
