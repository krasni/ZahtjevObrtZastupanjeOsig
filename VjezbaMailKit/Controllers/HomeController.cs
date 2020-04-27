using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VjezbaMailKit.Models;

namespace VjezbaMailKit.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailService _emailService;

        public HomeController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            // ajmo poslati mail
            var message = new EmailMessage
            {
                ToAddresses = new List<EmailAddress>
                {
                    new EmailAddress
                    {
                        //Name = "igor borota", Address = "igor.borota@hanfa.hr"
                        Name = "igor borota", Address = "borota.igor@gmail.com"
                    }
                },
                FromAddresses = new List<EmailAddress>
                 {
                    new EmailAddress
                    {
                        Name = "no reply", Address = "noreply@hanfa.hr"
                    }
                },
                Subject ="test",
                Content = "test"
            };
            _emailService.Send(message);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
