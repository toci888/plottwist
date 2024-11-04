using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
 using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class ChatGptApiClient
{
    private readonly string _apiKey;

    public ChatGptApiClient(string apiKey)
    {
        _apiKey = apiKey;
    }

public async Task<string> GenerateClassFromChatGPT(string prompt)
{
    using (var client = new HttpClient())
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");
        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "user", content = prompt }
            },
            max_tokens = 150
        };

        var response = await client.PostAsync("https://api.openai.com/v1/chat/completions",
            new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json"));

        var jsonResponse = await response.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.DeserializeObject(jsonResponse);
        return result.choices[0].message.content;
    }
}

    public async Task<string> GetResponseAsync(string prompt)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                new { role = "user", content = prompt }
            }
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic jsonResponse = JsonConvert.DeserializeObject(responseBody);
            return jsonResponse.choices[0].message.content.ToString();
        }
    }

    
}


