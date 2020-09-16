using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basics.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
        public IActionResult Authenticate()
        {

            var basicClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bob"),
                new Claim(ClaimTypes.Email,"faghirnvz@gmail.com"),
                new Claim("I.Say","Good")
            };
            var basicIdentity = new ClaimsIdentity(basicClaims, "bascic Identity");
            var LicenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,"faghirnvz@gmail.com"),
                new Claim("DrivingLicense","A+")
            };
            var LicenseIdentity = new ClaimsIdentity(LicenseClaims, "Government");
            var userPrincipal = new ClaimsPrincipal(new[] { basicIdentity,LicenseIdentity});

            HttpContext.SignInAsync(userPrincipal);
            //Claim is an abstract concept
            return RedirectToAction("Index");
        }



    }
}
