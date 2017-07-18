using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactApp.Models;

namespace ContactApp.DataHandlers
{
    /// <summary>
    /// Servira à gérer le mode hors-ligne en appelant le repo REST ou la db locale
    /// en fonction du réseau
    /// Pour l'instant : données mockées.
    /// </summary>
    public class ContactRedoLog : IRepository
    {
        private ContactRestRepository Repository;
        //private ContactFakeRepository Repository;

        public ContactRedoLog()
        {
            //Repository = new ContactFakeRepository();
            Repository = new ContactRestRepository();
        }

        public void addContact(Contact contact)
        {
            Repository.addContact(contact);
        }

        public void deleteContact(int id)
        {
            Repository.deleteContact(id);
        }

        public void editContact(int id, Contact contact)
        {
            Repository.editContact(id, contact);
        }

        public Task<List<Contact>> getAllContacts()
        {
            return Repository.getAllContacts();
        }

        public Task<Contact> getContact(int id)
        {
            return Repository.getContact(id);
        }
    }
}
