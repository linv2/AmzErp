using Newtonsoft.Json;

namespace AmazonSDK.Models
{
    public class AccessTokenResponseModel
    {
        /// <summary>
        /// 授权您的应用程序代表卖家采取某些操作的令牌
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        /// <summary>
        /// 访问令牌失效之前的秒数。
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        /// <summary>
        /// 可以交换为新访问令牌的长期令牌
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        /// <summary>
        /// 返回的令牌类型。应该是 bearer。
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
