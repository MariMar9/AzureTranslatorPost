using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    private static readonly string key = "263591fc6d2942a5a08d57cbf175fa14";
    private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";

    private static readonly string location = "westeurope";
    
    static async Task Main(string[] args)
    {
        string route = "/translate?api-version=3.0&from=es&to=en";
        Console.WriteLine("Introduzca el texto a traducir:");
        string textToTranslate = Console.ReadLine();
        object[] body = new object[] { new { Text = textToTranslate } };
        var requestBody = JsonConvert.SerializeObject(body);
    
        using (var client = new HttpClient())
        using (var request = new HttpRequestMessage())
        {
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(endpoint + route);
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            request.Headers.Add("Ocp-Apim-Subscription-Key", key);
            request.Headers.Add("Ocp-Apim-Subscription-Region", location);
    
            HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }
    }
}