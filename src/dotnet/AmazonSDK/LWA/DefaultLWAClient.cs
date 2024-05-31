using AmazonSDK.Models;
using AmazonSDK.StaticConfigResourse;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonSDK.LWA
{
    public class DefaultLWAClient : ILWAClient
    {
        internal const string AmzTokenUrl = "https://api.amazon.com/auth/o2/token";
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public DefaultLWAClient(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public int MaxErrorRetryNumber { get; set; } = 3;


        public AccessTokenResponseModel AccessToken(string code)
        {
            var restClient = new RestClient();
            var restRequest = new RestRequest(AmzTokenUrl, Method.POST);
            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            restRequest.AddParameter("grant_type", "authorization_code")
                .AddParameter("code", code)
                .AddParameter("client_id", ClientId)
                .AddParameter("client_secret", ClientSecret);

            int retryTimes = 1;
            var response = restClient.Execute(restRequest);

            while (response.StatusCode != System.Net.HttpStatusCode.OK && retryTimes < MaxErrorRetryNumber)
            {
                response = restClient.Execute(restRequest);
                retryTimes++;
            }
            return JsonConvert.DeserializeObject<AccessTokenResponseModel>(response.Content);
        }

        public AccessTokenResponseModel RefreshToken(string refresh_token)
        {

            var restClient = new RestClient();
            var restRequest = new RestRequest(AmzTokenUrl, Method.POST);
            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            restRequest.AddParameter("grant_type", "refresh_token")
                .AddParameter("refresh_token", refresh_token)
                .AddParameter("client_id", ClientId)
                .AddParameter("client_secret", ClientSecret);

            int retryTimes = 1;
            var response = restClient.Execute<AccessTokenResponseModel>(restRequest);
            while (response.StatusCode != System.Net.HttpStatusCode.OK && retryTimes < MaxErrorRetryNumber)
            {
                response = restClient.Execute<AccessTokenResponseModel>(restRequest);
                retryTimes++;
            }
            return response.Data;
        }

        public string GetAuthorizationUrl(string appId, string state = null, bool beta = true)
        {
            return $"https://sellercentral.amazon.com/apps/authorize/consent?application_id={appId}&state={state}{(beta ? "&version=beta" : null)}";
        }
    }
}
