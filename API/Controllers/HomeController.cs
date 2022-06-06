using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ixcent.CryptoTerminal.Api.Controllers
{
    /// <summary>
    /// Main controller.
    /// </summary>
    [AllowAnonymous]
    public class HomeController : Controller
    {
        /// <summary>
        /// Main webpage
        /// </summary>
        public string Index()
        {
            return "hello";
        }
    }
}