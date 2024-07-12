using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentationCreator.MVC.Models;

namespace PresentationCreator.MVC.Controllers
{
    public class PaymentController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7042/api");
        private readonly HttpClient _client;

        public PaymentController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<PaymentViewModel> paymentnList = new List<PaymentViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Notification/notifications").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                paymentnList = JsonConvert.DeserializeObject<List<PaymentViewModel>>(data)!;
            }
            return View(paymentnList);
        }
    }
}
