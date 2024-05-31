using Newtonsoft.Json;

namespace AmazonSDK.Models
{
    public class AccessTokenResponseModel
    {
        /// <summary>
        /// ��Ȩ����Ӧ�ó���������Ҳ�ȡĳЩ����������
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        /// <summary>
        /// ��������ʧЧ֮ǰ��������
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        /// <summary>
        /// ���Խ���Ϊ�·������Ƶĳ�������
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        /// <summary>
        /// ���ص��������͡�Ӧ���� bearer��
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
