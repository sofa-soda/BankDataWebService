using BankPresentationTier.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using RestSharp;
using Newtonsoft.Json;

namespace BankPresentationTier.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
