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
        float SwipeDistance = 0;
        float ReferenceSwipeDistance = 50;

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

        private View CreateContactElement(Contact contact)
        {
            var contactElement = new StackLayout { Orientation = StackOrientation.Horizontal };
            var elem = new StackLayout { Orientation = StackOrientation.Vertical, HorizontalOptions = LayoutOptions.FillAndExpand };
            elem.Children.Add(new Label { Text = contact.FirstName + " " + contact.LastName, VerticalOptions = LayoutOptions.Fill });
            elem.Children.Add(new Label { Text = contact.PhoneNumber, VerticalOptions = LayoutOptions.Fill });
            contactElement.Children.Add(elem);

            var buttonSMS = new Button { Text = "SMS", HorizontalOptions = LayoutOptions.Fill };
            buttonSMS.Clicked += GetSMSEventHandler(contact.PhoneNumber);
            contactElement.Children.Add(buttonSMS);
            var buttonTel = new Button { Text = "TEL", HorizontalOptions = LayoutOptions.Fill};
            contactElement.Children.Add(buttonTel);

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            contactElement.GestureRecognizers.Add(panGesture);

            return contactElement;
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
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

        private async void HandleTouch(View sender, float diff_x)
        {
            await sender.TranslateTo(diff_x, 0);
            SwipeDistance = diff_x;
        }

        private async void HandleTouchEnd(View sender)
        {
            if (Math.Abs(SwipeDistance) > ReferenceSwipeDistance)
            {
                // envoyer l'élément hors du champ
                await sender.TranslateTo(SwipeDistance > 0 ? this.Width : -this.Width, 0);
                if (SwipeDistance > 0)
                {
                    SwipedRight(sender);
                }
                else
                {
                    SwipedLeft(sender);
                }
            }
            else
            {
                // remettre l'élément à sa position initiale
                await sender.TranslateTo(-sender.X, 0);
                SwipeDistance = 0;
            }
        }

        private void SwipedRight(View sender)
        {

        }

        private void SwipedLeft(View sender)
        {

        }
        
        private EventHandler GetSMSEventHandler(string PhoneNumber)
        {
            // les EventHandler doivent avoir pour paramètres (object sender, EventArgs e)
            // ici on renvoie une fonction de type EventHandler, qui appellera SMS_Clicked avec 
            // le bon paramètre PhoneNumber
            return (sender, e) => SMS_Clicked(PhoneNumber);
        }

        private void SMS_Clicked(string PhoneNumber)
        {
            DependencyService.Get<ICellPhone>().openSMS(PhoneNumber);
        }
    }
}
