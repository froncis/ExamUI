using Exam.Mvc.Web.Common;
using Exam.Mvc.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exam.Mvc.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(AccountLoginModel accountLoginModel)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Account/authenticate", accountLoginModel).Result;

            AuthenticationResponseModel authenticationResponseModel = new AuthenticationResponseModel();

            return View();
        }

        public IActionResult Register(AccountRegistrationModel accountRegistrationModel)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Account/register", accountRegistrationModel).Result;
            return View();
        }
    }
}
