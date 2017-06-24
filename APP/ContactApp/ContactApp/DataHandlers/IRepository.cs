using ContactApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.DataHandlers
{
    public interface IRepository
    {
        List<Contact> getAllContacts();
        Contact getContact(int id);
        void addContact(Contact contact);
        void editContact(int id, Contact contact);
        void deleteContact(int id);
    }
}
