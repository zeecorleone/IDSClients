using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherMVC.Models;
using WeatherMVC.Services;

namespace WeatherMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger,
            ITokenService tokenService,
            IConfiguration configuration)
        {
            _logger = logger;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
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

        [Authorize]
        public async Task<IActionResult> Weather()
        {
            using var client = new HttpClient();

            //for m2m client:
            //var token = await _tokenService.GetToken("weatherapi.read");

            //for interactive client:
            var token = await HttpContext.GetTokenAsync("access_token");

            client.SetBearerToken(token);
            var weatherApiUrl = _configuration["WeatherAPIUrl"];
            var result = await client.GetAsync(weatherApiUrl);
            if(result.IsSuccessStatusCode)
            {
                var data = await result.Content.ReadAsStringAsync();
                var weather = System.Text.Json.JsonSerializer.Deserialize<List<WeatherData>>(data);
                return View(weather);
            }

            throw new Exception("Error! Unable to get data!");
        }
    }
}