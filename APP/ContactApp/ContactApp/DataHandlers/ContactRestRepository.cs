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
            var URL = Config.GetBaseURL();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromMinutes(3);
        }

        public async void addContact(Contact contact)
        {
            throw new NotImplementedException();
            //HttpResponseMessage response = await client.PostAsJsonAsync("", contact);
            //response.EnsureSuccessStatusCode();

            //// Return the URI of the created resource.
            //var i = response.Headers.Location;
        }

        public void deleteContact(int id)
        {
            throw new NotImplementedException();
            //client.DeleteAsync($"/{id}");
        }

        public void editContact(int id, Contact contact)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Contact>> getAllContacts()
        {
                List<Contact> list = new List<Contact>();
            try
            {
                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    list = await response.Content.ReadAsAsync<List<Contact>>();
                }
            }

            catch (Exception e)
            {
                
            }
                return list;
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
