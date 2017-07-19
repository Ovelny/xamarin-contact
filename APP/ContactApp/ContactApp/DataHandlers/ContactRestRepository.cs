using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactApp.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

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
            client.BaseAddress = new Uri(Config.GetBaseURL());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<int> addContact(Contact contact)
        {
            var content = new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("contacts", content);
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception("Erreur lors de l'enregistrement");
            }
            else
            {
                var savedContact = await response.Content.ReadAsAsync<Contact>();
                return savedContact.Id;
            }
        }

        public async Task deleteContact(int id)
        {
            await client.DeleteAsync($"contacts/{id}");
        }

        public async Task editContact(int id, Contact contact)
        {
            var content = new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"contacts/{id}", content);
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception("Erreur lors de l'enregistrement");
            }
        }

        public async Task<List<Contact>> getAllContacts()
        {
            List<Contact> list = new List<Contact>();
            HttpResponseMessage response = await client.GetAsync("contacts");
            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadAsAsync<List<Contact>>();
            }
            return list;
        }

        public async Task<Contact> getContact(int id)
        {
            Contact contact = null;
            HttpResponseMessage response = await client.GetAsync($"contacts/{id}");
            if (response.IsSuccessStatusCode)
            {
                contact = await response.Content.ReadAsAsync<Contact>();
            }
            return contact;
        }
    }
}
