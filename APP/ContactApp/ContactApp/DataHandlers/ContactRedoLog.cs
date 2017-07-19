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

        public Task addContact(Contact contact)
        {
            return Repository.addContact(contact);
        }

        public Task deleteContact(int id)
        {
            return Repository.deleteContact(id);
        }

        public Task editContact(int id, Contact contact)
        {
            return Repository.editContact(id, contact);
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
