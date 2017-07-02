using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactApp.Models;

namespace ContactApp.DataHandlers
{
    public class ContactFakeRepository : IRepository
    {
        private List<Contact> contacts = new List<Contact>
            {
                new Contact
                {
                    Id = 1,
                    FirstName = "Michel",
                    LastName = "Durand",
                    PhoneNumber = "06 12 34 56 78"
                },new Contact
                {
                    Id = 2,
                    FirstName = "Martine",
                    LastName = "Dupond",
                    PhoneNumber = "06 12 34 56 78"
                },new Contact
                {
                    Id = 3,
                    FirstName = "Rémi",
                    LastName = "Sansfamille",
                    PhoneNumber = "06 12 34 56 78"
                },new Contact
                {
                    Id = 4,
                    FirstName = "Jeanne",
                    LastName = "Esserge",
                    PhoneNumber = "06 12 34 56 78"
                },new Contact
                {
                    Id = 5,
                    FirstName = "Jean-Paul",
                    LastName = "Personne",
                    PhoneNumber = "06 12 34 56 78"
                }
            };

        public void addContact(Contact contact)
        {
            this.contacts.Add(contact);
        }

        public void deleteContact(int id)
        {
            var item = contacts.FirstOrDefault(x => x.Id == id);
            if (item != null)
                this.contacts.Remove(item);
        }

        public void editContact(int id, Contact contact)
        {
            var item = contacts.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.Address = contact.Address;
                item.FirstName = contact.FirstName;
                item.LastName= contact.LastName;
                item.Mail = contact.Mail;
                item.PhoneNumber = contact.PhoneNumber;
                item.Photos = contact.Photos;
            }
        }

        public List<Contact> getAllContacts()
        {
            return contacts;
        }

        public Contact getContact(int id)
        {
            return contacts.FirstOrDefault(c => c.Id == id);
        }
    }
}
