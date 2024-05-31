using AmazonSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonSDK
{
    public delegate void OnDebug(string message);
    public interface IAWSClient
    {
        event OnDebug DebugEvent;
        AWSAuthenticationCredentials Credentials { get; set; }
        TResponse Request<TResponse>(AmazonRequestModel<TResponse> requestModel, string accessToken);
        string RequestString<TRequest>(TRequest requestModel, string accessToken) where TRequest : AmazonRequestModel;
    }
}
