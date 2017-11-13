using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace SPG.Models
{
    public class Bulletins
    {
        private Config config;

        public Bulletins (Config config)
        {
            this.config = config;
        }

        private HttpResponseMessage requestToSHG(string serializedObjectData, string actioUri)
        {
            var client = new HttpClient();
            var content = new StringContent(serializedObjectData, Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(config.SHG_URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client.PostAsync(actioUri, content).Result;
        }
        public bool sendBulletin(int userId, string data, string signature, string signaturePubExponent, string signatureModulus)
        {
            string serializedObjectData = JsonConvert.SerializeObject(
                new {
                    userId = userId, data = data, signature = signature, signaturePubExponent = signaturePubExponent, signatureModulus = signatureModulus
                });
            HttpResponseMessage response = requestToSHG(serializedObjectData, "api/shg/save-bulletin");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
