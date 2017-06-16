using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using xamarinContact.Data;
using xamarinContact.Exceptions;
using xamarinContact.Models;

namespace xamarinContact.Services
{
    public class ContactsStore
    {
        public List<Contact> AllContacts()
        {
            using (var context = new xamarinContactModel())
            {
                return context.Contacts.ToList();
            }
        }

        public Contact ContactById(int id)
        {
            using (var context = new xamarinContactModel())
            {
                return context.Contacts.Find(id);
            }
        }

        public Contact Create(Contact contact)
        {
            using (var context = new xamarinContactModel())
            {
                var newContact = context.Contacts.Add(contact);
                context.SaveChanges();
                return newContact;
            }
        }

        public Contact Update(int id, Contact contact)
        {
            using (var context = new xamarinContactModel())
            {
                var oldContact = context.Contacts.Find(id);
                if (oldContact == null)
                    throw new ContactNotFoundException();
                oldContact.Address = contact.Address;
                oldContact.FirstName = contact.FirstName;
                oldContact.LastName = contact.LastName;
                oldContact.Mail = contact.Mail;
                oldContact.PhoneNumber = contact.PhoneNumber;
                oldContact.Photos = contact.Photos;
                context.SaveChanges();
                return oldContact;
            }
        }

        public void Delete(int id)
        {
            using (var context = new xamarinContactModel())
            {
                var contact = context.Contacts.Find(id);
                if (contact == null)
                    throw new ContactNotFoundException();
                context.Contacts.Remove(contact);
                context.SaveChanges();
            }
        }
    }
}