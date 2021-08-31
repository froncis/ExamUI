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
    public class CalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Compute(CalculatorModel model)
        {
            IEnumerable<CalculatorModel> calculatorModels;

            var queryString = "PresentValue=" + model.PresentValue +
                                "&LowerBoundInterestRate=" + model.LowerBoundInterestRate +
                                "&MaturityYear=" + model.MaturityYear +
                                "&UpperBoundInterestRate=" + model.UpperBoundInterestRate +
                                "&IncrementalRate=" + model.IncrementalRate;

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Calculator?" + queryString).Result;
            calculatorModels = response.Content.ReadAsAsync<IEnumerable<CalculatorModel>>().Result;

            foreach (var item in calculatorModels)
            {
                item.LowerBoundInterestRate *= 100;
                item.UpperBoundInterestRate *= 100;
                item.IncrementalRate *= 100;
            }

            return PartialView("_computedResultPartialView", calculatorModels);
        }

        public IActionResult SaveComputed(CalculatorModel model)
        {
            IEnumerable<CalculatorModel> calculatorModels;

            var queryString = "PresentValue=" + model.PresentValue +
                                "&LowerBoundInterestRate=" + model.LowerBoundInterestRate +
                                "&MaturityYear=" + model.MaturityYear +
                                "&UpperBoundInterestRate=" + model.UpperBoundInterestRate +
                                "&IncrementalRate=" + model.IncrementalRate;

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Calculator?" + queryString).Result;
            calculatorModels = response.Content.ReadAsAsync<IEnumerable<CalculatorModel>>().Result;

            List<CalculatorValueModel> modelToSave = new List<CalculatorValueModel>();

            foreach (var item in calculatorModels)
            {
                CalculatorValueModel valueModel = new CalculatorValueModel();
                valueModel.PresentValue = item.PresentValue;
                valueModel.FutureValue = item.FutureValue;
                valueModel.Year = item.Year;
                valueModel.LowerBoundInterestRate = item.LowerBoundInterestRate;
                modelToSave.Add(valueModel);
            }

            HttpResponseMessage saveModel = GlobalVariables.WebApiClient.PostAsJsonAsync("CalculatorValue", modelToSave).Result;

            return RedirectToAction("Index");
        }
    }
}
