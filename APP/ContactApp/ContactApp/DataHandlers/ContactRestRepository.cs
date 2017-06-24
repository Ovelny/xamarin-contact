using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactApp.Models;

namespace ContactApp.DataHandlers
{
    /// <summary>
    /// Gère le repository REST (API)
    /// </summary>
    public class ContactRestRepository : IRepository
    {
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
            throw new NotImplementedException();
        }

        public Contact getContact(int id)
        {
            throw new NotImplementedException();
        }
    }
}
