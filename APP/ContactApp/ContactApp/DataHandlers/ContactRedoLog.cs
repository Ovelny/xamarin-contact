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
        private ContactRestRepository RestRepository;
        private ContactFakeRepository FakeRepository;

        public ContactRedoLog()
        {
            FakeRepository = new ContactFakeRepository();
        }

        public void addContact(Contact contact)
        {
            FakeRepository.addContact(contact);
        }

        public void deleteContact(int id)
        {
            FakeRepository.deleteContact(id);
        }

        public void editContact(int id, Contact contact)
        {
            FakeRepository.editContact(id, contact);
        }

        public List<Contact> getAllContacts()
        {
            return FakeRepository.getAllContacts();
        }

        public Contact getContact(int id)
        {
            return FakeRepository.getContact(id);
        }
    }
}
