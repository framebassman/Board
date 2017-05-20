using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Board.Server.Models;
using RestSharp;

namespace Board.Server.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var li = new List<VirutalMachine>();
            var client = new RestClient();
            var request = new RestRequest("api/virtualmachineinfo");
            string[] servers = ConfigurationManager.AppSettings["Servers"].Replace(" ", "").Split(',');

            foreach (var server in servers)
            {
                client.BaseUrl = new Uri($"http://{server}/Board.Client");
                var response = client.Execute<VirutalMachine>(request);
                if (string.IsNullOrEmpty(response.ErrorMessage))
                    li.Add(response.Data);
            }

            ViewBag.VMs = li;

            return View();
        }
    }
}