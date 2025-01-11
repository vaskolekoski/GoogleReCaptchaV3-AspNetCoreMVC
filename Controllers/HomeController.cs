using GoogleReCaptchaV3.Models;
using GoogleReCaptchaV3.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GoogleReCaptchaV3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                string googleRecaptchaToken = Request.Form["g-recaptcha-response"].ToString();

                //verify the token 
                string secretKey = _configuration["ReCaptchaSettings:SecretKey"]!;
                string verificationUrl = _configuration["ReCaptchaSettings:VerificationUrl"]!;
                bool isValid = await RecaptchaService.verifyReCaptchaV3(googleRecaptchaToken, secretKey, verificationUrl);

                if (!isValid)
                {
                    //error
                    model.errorMessage = "Unvalid Recaptcha";

                }
                else
                {
                    model.successMessage = "Valid recaptcha";
                }
            }
            else
            {
                //error
                model.errorMessage = "Form is not valid";
            }
            return View(model);
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
