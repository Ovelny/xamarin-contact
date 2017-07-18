using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactApp.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ContactApp.DataHandlers
{
    /// <summary>
    /// Gère le repository REST (API)
    /// </summary>
    public class ContactRestRepository : IRepository
    {
        private static HttpClient client = new HttpClient();

        public ContactRestRepository()
        {
            client.BaseAddress = new Uri("http://192.168.43.215:64342/api/contacts");
            //client.BaseAddress = new Uri("http://localhost:64342/api/contacts");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromMinutes(3);
        }

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

        public async Task<List<Contact>> getAllContacts()
        {
                //Contact contact = new Contact { Address = "aa", LastName = "bb", Mail = "cc", FirstName = "dd" };
                //HttpResponseMessage res = await client.PostAsJsonAsync("", contact);
                //res.EnsureSuccessStatusCode();
            //return null;
            try
            {
                List<Contact> list = new List<Contact>();
                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    list = await response.Content.ReadAsAsync<List<Contact>>();
                }
                return list;

            }catch(Exception e)
            {

            }
            return null;
        }

        public async Task<Contact> getContact(int id)
        {
            Contact contact = null;
            HttpResponseMessage response = await client.GetAsync("/"+id.ToString());
            if (response.IsSuccessStatusCode)
            {
                contact = await response.Content.ReadAsAsync<Contact>();
            }
            return contact;
        }
    }
}
