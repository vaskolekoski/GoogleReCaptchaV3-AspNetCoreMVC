using System.Text.Json.Nodes;

namespace GoogleReCaptchaV3.Services
{
    public class RecaptchaService
    {
        public static async Task<bool> verifyReCaptchaV3(string response, string secret, string verificationUrl)
        {
            using (var client = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(response), "response");
                content.Add(new StringContent(secret), "secret");

                var result = await client.PostAsync(verificationUrl, content);

                if (result.IsSuccessStatusCode)
                {
                    var strResponse = await result.Content.ReadAsStringAsync();
                    Console.WriteLine(strResponse);

                    var jsonResponse = JsonNode.Parse(strResponse);
                    if (jsonResponse != null)
                    {
                        var success = ((bool?)jsonResponse["success"]);
                        if (success != null && success == true) return true;
                    }
                }
            }

            return false;
        }
    }
}
