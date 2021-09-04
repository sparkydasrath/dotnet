using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace HttpClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Saleforce Auth");
            GetToken();
            Console.ReadLine();
        }

        private static async void GetToken()
        {
            /*
             *
             *
             * https://test.salesforce.com
                services/oauth2/token
             */



            Dictionary<string, string> requestBody = new Dictionary<string, string>
            {
                {"username", "sparky.dasrath@blackstone.com.sparky3"},
                {"password", "$Dsf2020p0"},
                {"client_id", "3MVG9QDx8IX8nP5SH4hnvFVKfh8gxFr9aPryL7S5WvZL2NheJj0wCK2XXo.zlOnFr_fq_x77op7Wtg0T8copt"},
                {"client_secret", "5084274229796883739"},
                {"grant_type", "password"},
                //{"auth_token_endpoint", @"https://test.salesforce.com/services/oauth2/token"}


            };



            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(@"https://test.salesforce.com/services/oauth2/token"))
            {
                Content = new FormUrlEncodedContent(requestBody)
            };

            using HttpClient client = new HttpClient();

            try
            {
                var responseMessage = await client.SendAsync(request).ConfigureAwait(false);
                string response = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                Console.WriteLine("Response \n" + response);
                string json = JsonSerializer.Serialize(response);
                Console.WriteLine(json);
                Console.WriteLine("bleh");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
