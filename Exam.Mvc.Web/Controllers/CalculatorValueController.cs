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
    public class CalculatorValueController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<CalculatorValueModel> calculatorValueModels;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("CalculatorValue/all").Result;

            if (response.IsSuccessStatusCode)
            {
                return View(calculatorValueModels = response.Content.ReadAsAsync<IEnumerable<CalculatorValueModel>>().Result);
            }
            else
            {
                calculatorValueModels = Enumerable.Empty<CalculatorValueModel>();
                ModelState.AddModelError(string.Empty, "Server error. No response from the API.");
            }

            return View(calculatorValueModels);
        }
    }
}
