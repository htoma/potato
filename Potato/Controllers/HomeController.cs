using System.Diagnostics;
using Azure;
using Azure.EntityManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Potato.Models;

namespace Potato.Controllers
{
    public class HomeController : BaseController
    {
        private readonly UserManager _userManager;

        public HomeController(IOptions<AzureSettings> azure) : base(azure)
        {
            _userManager = new UserManager(azure.Value.Cosmos.EndpointUrl, azure.Value.Cosmos.Key);
        }

        public IActionResult Index()
        {
            _userManager.GetUsers();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Dress up for match day";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "friends@senorpota.to";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class BaseController : Controller
    {
        protected readonly AzureManager Manager;

        public BaseController(IOptions<AzureSettings> azure)
        {
            Manager = new AzureManager(azure.Value.Cosmos.EndpointUrl, azure.Value.Cosmos.Key);
        }
    }
}
