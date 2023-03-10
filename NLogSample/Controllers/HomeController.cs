using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLogSample.Models;
using System.Diagnostics;

namespace NLogSample.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> Logger { get; }
        private TelemetryClient TelemetryClient { get; }

        public HomeController(ILogger<HomeController> logger, TelemetryClient telemetryClient)
        {
            Logger = logger;
            TelemetryClient = telemetryClient;
        }

        public IActionResult Index()
        {
            using var op = TelemetryClient.StartOperation<DependencyTelemetry>("Index");
            op.Telemetry.Type = "Home Controller";
            Logger.LogInformation("Hello!");
            Logger.LogWarning("You have been warned, {User}", new { Name = "John Doe" });
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
