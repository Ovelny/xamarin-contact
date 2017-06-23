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
        float SwipeDistance = 0;
        float ReferenceSwipeDistance = 30;

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

        protected View CreateContactElement(Contact contact)
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

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            contactElement.GestureRecognizers.Add(panGesture);

            return contactElement;
        }

        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    //HandleTouchStart();
                    break;
                case GestureStatus.Running:
                    HandleTouch((View)sender, (float)e.TotalX);
                    break;
                case GestureStatus.Completed:
                    HandleTouchEnd((View)sender);
                    break;
            }
        }

        async void HandleTouch(View sender, float diff_x)
        {
            await sender.TranslateTo(diff_x, 0);
            SwipeDistance = diff_x;
        }

        async void HandleTouchEnd(View sender)
        {
            //if(Math.Abs(SwipeDistance) > ReferenceSwipeDistance)
            //{
            //  // envoyer l'élément hors du champ
            //}
            //else
            {
                // remettre l'élément à sa position initiale
                await sender.TranslateTo(-sender.X, 0);
                SwipeDistance = 0;
            }
        }
    }
}
