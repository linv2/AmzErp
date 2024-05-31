using AmazonSDK.Attribute;
using AmazonSDK.Models;
using AmazonSDK.SDKException;
using AmazonSDK.Signer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text;

namespace AmazonSDK
{
    public class AWSClient : IAWSClient
    {
        public event OnDebug DebugEvent;
        public AWSAuthenticationCredentials Credentials { get; set; }

        private AWSSigV4Signer AwsSigV4Signer { get; set; }
        internal AWSClient(AWSAuthenticationCredentials credentials)
        {
            this.Credentials = credentials;
            AwsSigV4Signer = new AWSSigV4Signer(this);
        }


        public static IAWSClient Create(AWSAuthenticationCredentials credentials)
        {
            return new AWSClient(credentials);
        }


        public TResponse Request<TResponse>(AmazonRequestModel<TResponse> requestModel, string accessToken)
        {

            var apiResource = requestModel.GetAPIResource();
            var restClient = new RestClient(Credentials.SellerRegion.EndPoint);
            var restRequest = (IRestRequest)new RestRequest(apiResource.ResourceUri, apiResource.Method);
            restRequest.OnBeforeRequest = http =>
            {
                BeforRequest(restRequest, http);
            };
            if (apiResource.Method != Method.GET)
            {
                restRequest.AddJsonBody(requestModel);
            }
            else
            {
                var jObject = JObject.FromObject(requestModel);
                var properties = jObject.Properties();
                foreach (var property in properties)
                {
                    restRequest.AddQueryParameter(property.Name, property.Value<string>());
                }
            }
            restRequest = AwsSigV4Signer.Sign(restRequest, Credentials.SellerRegion.EndPoint.Host, accessToken);
            int retryTimes = 1;
            var response = restClient.Execute<AmazonResponseModel<TResponse>>(restRequest);
            RequestComplate(response);
            while (response.StatusCode != System.Net.HttpStatusCode.OK && retryTimes < 3)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    AmazonResponseError amazonResponseError = JsonConvert.DeserializeObject<AmazonResponseError>(response.Content);
                    throw new ClientException(amazonResponseError);
                }
                response = restClient.Execute<AmazonResponseModel<TResponse>>(restRequest);
                RequestComplate(response);
                retryTimes++;
            }
            if (retryTimes >= 3)
            {
                DebugEvent?.Invoke($"{this.Credentials.SellerRegion.EndPoint}{apiResource.ResourceUri} retry 3 times faild");
            }
            return response.Data.PayLoad;
        }

        public string RequestString<TRequest>(TRequest requestModel, string accessToken) where TRequest : AmazonRequestModel
        {
            var apiResource = requestModel.GetAPIResource();
            var restClient = new RestClient(Credentials.SellerRegion.EndPoint);
            var restRequest = (IRestRequest)new RestRequest(apiResource.ResourceUri, apiResource.Method);
            if (apiResource.Method != Method.GET)
            {
                restRequest.AddJsonBody(JsonConvert.SerializeObject(requestModel));
            }
            else
            {
                var jObject = JObject.FromObject(requestModel);
                var properties = jObject.Properties();
                foreach (var property in properties)
                {

                    if (property.Value.Type == JTokenType.String)
                        restRequest.AddQueryParameter(property.Name, property.Value.Value<string>());
                }
            }
            restRequest.AddHeader("Content-Type", "application/json");
            //restRequest.AddQueryParameter("MarketplaceIds", "A21TJRUUN4KGV");
            restRequest = AwsSigV4Signer.Sign(restRequest, Credentials.SellerRegion.EndPoint.Host, accessToken);
            restRequest.OnBeforeRequest = http =>
            {
                BeforRequest(restRequest, http);
            };
            int retryTimes = 1;

            var response = restClient.Execute(restRequest);
            RequestComplate(response);
            while (response.StatusCode != System.Net.HttpStatusCode.OK && retryTimes < 3)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    AmazonResponseError amazonResponseError = JsonConvert.DeserializeObject<AmazonResponseError>(response.Content);
                    throw new ClientException(amazonResponseError);
                }
                response = restClient.Execute(restRequest);
                RequestComplate(response);
                retryTimes++;
            }
            if (retryTimes >= 3)
            {
                DebugEvent?.Invoke($"{this.Credentials.SellerRegion.EndPoint}{apiResource.ResourceUri} retry 3 times faild");
            }
            return response.Content;
        }

        private void BeforRequest(IRestRequest restRequest, IHttp http)
        {
            if (DebugEvent != null)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine().AppendLine($"{restRequest.Method} {http.Url}");
                foreach (var header in http.Headers)
                {
                    stringBuilder.AppendLine($"{header.Name}:{header.Value}");
                }
                if (!string.IsNullOrEmpty(http.RequestBody))
                {
                    stringBuilder.AppendLine().Append(http.RequestBody);
                }
                DebugEvent?.Invoke(stringBuilder.ToString());
            }
        }

        private void RequestComplate(IRestResponse restResponse)
        {
            if (DebugEvent != null)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine().AppendLine($"StatusCode={restResponse.StatusCode}");
                foreach (var header in restResponse.Headers)
                {
                    stringBuilder.AppendLine($"{header.Name}:{header.Value}");
                }
                stringBuilder.AppendLine().Append(restResponse.Content); 
                DebugEvent?.Invoke(stringBuilder.ToString());
            }
        }
    }
}
