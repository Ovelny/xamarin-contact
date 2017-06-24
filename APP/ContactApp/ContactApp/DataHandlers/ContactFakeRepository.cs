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
                }
            };

        public void addContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public void deleteContact(int id)
        {
            throw new NotImplementedException();
        }

        public void editContact(int id, Contact contact)
        {
            throw new NotImplementedException();
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
