using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using xamarinContact.Data;
using xamarinContact.Exceptions;
using xamarinContact.Models;
using xamarinContact.Services;

namespace xamarinContact.Controllers
{
    public class ContactsController : ApiController
    {
        private ContactsStore store;

        public ContactsController()
        {
            this.store = new ContactsStore();
        }

        // GET: api/Contacts
        public List<Contact> GetContacts()
        {
            return this.store.AllContacts();
        }

        // GET: api/Contacts/5
        public Contact GetContact(int id)
        {
            var contact = this.store.ContactById(id);
            if (contact == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return contact;
        }

        // PUT: api/Contacts/5
        [HttpPut]
        public Contact Update(int id, Contact contact)
        {
            try
            {
                return this.store.Update(id, contact);
            }
            catch (ContactNotFoundException e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        // POST: api/Contacts
        [HttpPost]
        public Contact Create(Contact contact)
        {
            return this.store.Create(contact);
        }

        // DELETE: api/Contacts/5
        public void Delete(int id)
        {
            try
            {
                this.store.Delete(id);
            }
            catch (ContactNotFoundException e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}