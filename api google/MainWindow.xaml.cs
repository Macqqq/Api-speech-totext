using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace api_google
{
    class Program
    {
        static void Main(string[] args)
        {
            // Votre clé d'API
            string apiKey = "AIzaSyDlzxVyYU2gAhaECs8QVFtlrJnJJzjQY9s";

            // URL de l'API
            string url = $"https://speech.googleapis.com/v1/speech:recognize?key={apiKey}";

            // Créez l'objet HttpClient
            HttpClient client = new HttpClient();

            // Remplacez le chemin du fichier audio que vous souhaitez envoyer à l'API
            string audioFilePath = "enregistrement.wav";

            // Préparez le corps de la requête
            var requestBody = new
            {
                config = new
                {
                    encoding = "LINEAR16",
                    sampleRateHertz = 44100,
                    languageCode = "fr-FR"
                },
                audio = new
                {
                    content = Convert.ToBase64String(System.IO.File.ReadAllBytes(audioFilePath))
                }
            };

            // Convertissez l'objet en JSON
            string json = JsonConvert.SerializeObject(requestBody);

            // Préparez la requête HTTP
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            // Envoyez la requête à l'API et obtenez la réponse
            HttpResponseMessage response = client.SendAsync(request).Result;
            string responseContent = response.Content.ReadAsStringAsync().Result;

            // Affichez la réponse
            Console.WriteLine(responseContent);
        }
    }
}
