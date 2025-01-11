using System.ComponentModel.DataAnnotations;

namespace GoogleReCaptchaV3.Models
{
    public class LoginModel
    {
        private readonly IConfiguration _configuration;
        public LoginModel() { }
        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";

        [Required]
        public string Subject { get; set; } = "";
        [Required]
        public string Message { get; set; } = "";

        public string errorMessage { get; set; } = "";
        public string successMessage { get; set; } = "";
    }
}
