using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentationCreator.MVC.Models;

namespace PresentationCreator.MVC.Controllers;

public class NotificationController : Controller
{
    Uri baseAddress = new Uri("https://localhost:7042/api");
    private readonly HttpClient _client;

    public NotificationController()
    {
        _client = new HttpClient();
        _client.BaseAddress = baseAddress;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<NotificationViewModel> notificationList = new List<NotificationViewModel>();
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Notification/notifications").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            notificationList = JsonConvert.DeserializeObject<List<NotificationViewModel>>(data);
        }
        return View(notificationList);
    }
}
