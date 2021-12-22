using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoTerminal.Controllers
{
    public class TestsController: Controller
    {
        private string _authToken;

        public async Task<string> TestLogin()
        {
            var stringPayload = JsonSerializer.Serialize(new { username = "admin", password = "qwerty" });

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {

                // Do the actual request and await the response
                var httpResponse = await httpClient.PostAsync($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/users/login", httpContent);

                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();


                    if (!string.IsNullOrEmpty(responseContent))
                    {
                        _authToken = JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent)["access_token"];
                        return "OK";
                    }
                    // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
                }
            }

            return "FAIL";
        }

        public async Task<string> TestRegister()
        {
            
            var stringPayload = JsonSerializer.Serialize(new { email="1@1", username = "Pasha", role = 1, password = "papa12345" });

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authToken);
                // Do the actual request and await the response
                var httpResponse = await httpClient.PostAsync($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/users/register", httpContent);

                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    return responseContent;
                    // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
                }
            }

            return "FAIL";
        }
    }
}
