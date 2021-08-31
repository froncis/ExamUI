using Exam.Mvc.Web.Common;
using Exam.Mvc.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Mvc.Web.Controllers
{
    public class RateValueController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<RateValueModel> rateValueModels;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("RateValue/all").Result;

            if (response.IsSuccessStatusCode)
            {
               return View(rateValueModels = response.Content.ReadAsAsync<IEnumerable<RateValueModel>>().Result);
            }
            else
            {
                rateValueModels = Enumerable.Empty<RateValueModel>();
                ModelState.AddModelError(string.Empty, "Server error. No response from the API.");
            }

            return View(rateValueModels);
        }
        public IActionResult Edit(int id)
        {
            return View();
        }
        public IActionResult Update(RateValueModel rateValueModel)
        {

            HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("RateValue", rateValueModel).Result;
            return Redirect("Index");
        }
        public IActionResult Delete(int id)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("https://localhost:44327/api/RateValue/" + id),
                Content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json")
            };
            HttpResponseMessage response = GlobalVariables.WebApiClient.SendAsync(request).Result;
            return View();
        }
    }
}
