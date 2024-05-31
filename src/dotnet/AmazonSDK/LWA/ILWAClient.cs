using AmazonSDK.Models;
using AmazonSDK.StaticConfigResourse;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonSDK.LWA
{
    public interface ILWAClient
    {
        public int MaxErrorRetryNumber { get; set; }
        public string ClientId { get; }
        public string ClientSecret { get; }
        public AccessTokenResponseModel AccessToken(string code);
        public AccessTokenResponseModel RefreshToken(string refresh_token);

        string GetAuthorizationUrl(string appId, string state = null, bool beta = true);
    }
}
