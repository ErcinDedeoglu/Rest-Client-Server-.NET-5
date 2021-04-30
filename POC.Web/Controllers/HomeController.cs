using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using POC.DTO;
using POC.Helper;

namespace POC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpHelper _httpHelper;

        public HomeController(ILogger<HomeController> logger, Helper.HttpHelper httpHelper)
        {
            _logger = logger;
            _httpHelper = httpHelper;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var weatherForecast = await _httpHelper.Get<WeatherForecastDto[]>("WeatherForecast", cancellationToken);
            weatherForecast = await _httpHelper.Post<WeatherForecastDto[]>("WeatherForecast", weatherForecast, cancellationToken);
            return View(weatherForecast);
        }
    }
}
