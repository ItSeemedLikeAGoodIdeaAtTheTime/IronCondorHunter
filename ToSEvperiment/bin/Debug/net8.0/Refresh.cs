﻿using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;



namespace ToSEvperiment
{
    public class Refresh
    {

        //   private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        private static async Task RefreshTokens()
        {
            //        Logger.Info("Initializing...");

            string appKey = Environment.GetEnvironmentVariable("APP_KEY");
            string appSecret = Environment.GetEnvironmentVariable("APP_SECRET");

            // You can pull this from a local file,
            // Google Cloud Firestore/Secret Manager, etc.
            string refreshTokenValue = "Your-Refresh-Token";

            var payload = new Dictionary<string, string>
        {
            { "grant_type", "refresh_token" },
            { "refresh_token", refreshTokenValue }
        };

            var authenticationString = $"{appKey}:{appSecret}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));

            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(payload);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

                var response = await client.PostAsync("https://api.schwabapi.com/v1/oauth/token", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Retrieved new tokens successfully using refresh token.");
                }
                else
                {
                    Console.WriteLine($"Error refreshing access token: {await response.Content.ReadAsStringAsync()}");
                    return;
                }

                var responseString = await response.Content.ReadAsStringAsync();
                var refreshTokenDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);

                Console.WriteLine("Token dict refreshed.");
                Console.WriteLine(JsonConvert.SerializeObject(refreshTokenDict, Newtonsoft.Json.Formatting.Indented));
            }
        }
    }






}

