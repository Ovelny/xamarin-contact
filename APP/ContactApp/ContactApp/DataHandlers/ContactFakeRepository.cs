//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ContactApp.Models;

//namespace ContactApp.DataHandlers
//{
//    public class ContactFakeRepository : IRepository
//    {
//        private int idAuto = 5;
//        private List<Contact> contacts = new List<Contact>
//            {
//                new Contact
//                {
//                    Id = 1,
//                    FirstName = "Michel",
//                    LastName = "Durand",
//                    PhoneNumber = "06 12 34 56 78",
//                    //data:image/png;base64,
//                    Photos = "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"
//                },new Contact
//                {
//                    Id = 2,
//                    FirstName = "Martine",
//                    LastName = "Dupond",
//                    PhoneNumber = "06 12 34 56 78"
//                },new Contact
//                {
//                    Id = 3,
//                    FirstName = "Rémi",
//                    LastName = "Sansfamille",
//                    PhoneNumber = "06 12 34 56 78"
//                },new Contact
//                {
//                    Id = 4,
//                    FirstName = "Jeanne",
//                    LastName = "Esserge",
//                    PhoneNumber = "06 12 34 56 78"
//                },new Contact
//                {
//                    Id = 5,
//                    FirstName = "Jean-Paul",
//                    LastName = "Personne",
//                    PhoneNumber = "06 12 34 56 78"
//                }
//            };

//        public void addContact(Contact contact)
//        {
//            idAuto++;
//            contact.Id = idAuto;
//            this.contacts.Add(contact);
//        }

//        public void deleteContact(int id)
//        {
//            var item = contacts.FirstOrDefault(x => x.Id == id);
//            if (item != null)
//                this.contacts.Remove(item);
//        }

//        public void editContact(int id, Contact contact)
//        {
//            var item = contacts.FirstOrDefault(x => x.Id == id);
//            if (item != null)
//            {
//                item.Address = contact.Address;
//                item.FirstName = contact.FirstName;
//                item.LastName= contact.LastName;
//                item.Mail = contact.Mail;
//                item.PhoneNumber = contact.PhoneNumber;
//                item.Photos = contact.Photos;
//            }
//        }

//        public Task<List<Contact>> getAllContacts()
//        {
//            return new Task<List<Contact>>(() => { return contacts; });
//        }

//        public Task<Contact> getContact(int id)
//        {
//            return new Task<Contact>(() => { return contacts.FirstOrDefault(c => c.Id == id); });
//        }
//    }
//}
