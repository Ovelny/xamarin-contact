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
        Task<List<Contact>> getAllContacts();
        Task<Contact> getContact(int id);
        Task<int> addContact(Contact contact);
        Task editContact(int id, Contact contact);
        Task deleteContact(int id);
    }
}
