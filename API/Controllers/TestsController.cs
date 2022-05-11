using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Ixcent.CryptoTerminal.API.Controllers
{
    /// <summary>
    /// Controller for testing api elements.<para/>
    /// Url: <c>tests/</c>
    /// </summary>
    public class TestsController : Controller
    {
        private string _authToken;

        private IWebHostEnvironment _webHostEnvironment;

        public TestsController(IWebHostEnvironment env)
        {
            _webHostEnvironment = env;
        }

        /// <summary>
        /// Test for Login function. <para/>
        /// Test url: <c>tests/login</c> <br/>
        /// Login url: <c>api/login</c>
        /// </summary>
        /// <remarks> Works only in <c>Development</c> mode. </remarks>
        /// <returns><see cref="OkObjectResult"/> or <see cref="BadRequestObjectResult"/></returns>
        public async Task<IActionResult> Login()
        {
            if (!_webHostEnvironment.IsDevelopment()) return NotFound();

            var loginModel = new
            {
                usernameoremail = "pasha",
                password = "papa12345"
            };

            string stringPayload = JsonSerializer.Serialize(loginModel);

            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            using (HttpClient? httpClient = new HttpClient())
            {
                HttpResponseMessage? httpResponse = await httpClient.PostAsync(GetUrl("/api/users/login"), httpContent);

                if (httpResponse.Content != null)
                {
                    string? responseContent = await httpResponse.Content.ReadAsStringAsync();


                    if (!string.IsNullOrEmpty(responseContent))
                    {
                        _authToken = JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent)["access_token"];
                        return Ok();
                    }
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// Test for Register function. <para/>
        /// Test url: <c>tests/register</c> <br/>
        /// Register url: <c>api/register</c>
        /// </summary>
        /// <remarks> Works only in <c>Development</c> mode. </remarks>
        /// <returns><see cref="OkObjectResult"/> with response as a value or <see cref="BadRequestObjectResult"/></returns>
        public async Task<IActionResult> Register()
        {
            if (!_webHostEnvironment.IsDevelopment()) return NotFound();

            var registerModel = new
            {
                email = "Papa@Mama",
                username = "pasha",
                role = 1,
                password = "papa12345"
            };

            string stringPayload = JsonSerializer.Serialize(registerModel);

            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            using (HttpClient? httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);
                // Do the actual request and await the response
                HttpResponseMessage httpResponse = await httpClient.PostAsync(GetUrl("/api/users/register"), httpContent);

                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    return Ok(responseContent);
                }
            }

            return BadRequest();
        }

        private string GetUrl(string location)
        {
            return $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{location}";
        }
    }
}
