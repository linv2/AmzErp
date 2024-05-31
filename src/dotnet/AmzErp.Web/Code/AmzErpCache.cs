using AmzErp.Common.Attribute;
using AmzErp.DataAccess;
using CSRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions;
using Microsoft.Extensions.DependencyInjection;
using AmazonSDK.LWA;
using AmzErp.Common.Exception;
using AmazonSDK.Models;
using AmzErp.Common.Attribute.Service;

namespace AmzErp.Web.Code
{
    [SerivceSingleton]
    public class AmzErpCache
    {
        [Autowired]
        private IRedisClientAsync redisClient { get; set; }

        [Autowired]
        private IServiceProvider serviceProvider { get; set; }
        [Autowired]
        private AmzErp.DataAccess.AmzErpDbContext DbContext { get; set; }

        [Autowired]
        private ILWAClient LWAClient { get; set; }
        public async Task<string> GetAmzToken(int shopId)
        {
            const string AmzTokenPrefix = "AmazonSeller:Token:{0}";
            var redisKey = string.Format(AmzTokenPrefix, shopId);
            var result = await redisClient.GetAsync(redisKey);
            if (string.IsNullOrEmpty(result))
            {
                var accessTokenResponseModel = RequestAmazonToken(shopId);
                await redisClient.SetAsync(redisKey, accessTokenResponseModel.AccessToken, TimeSpan.FromSeconds(accessTokenResponseModel.ExpiresIn - 100));
                result = accessTokenResponseModel.AccessToken;
            }
            return result;
        }
        private AccessTokenResponseModel RequestAmazonToken(int shopId)
        {
            using (var DbContent = serviceProvider.CreateScope().ServiceProvider.GetService<AmzErpDbContext>())
            {
                var amzShopId = DbContent.AmzShop.FirstOrDefault(x => x.Id == shopId);

                var responseModel = LWAClient.RefreshToken(amzShopId.RefreshToken);
                if (responseModel != null && string.IsNullOrEmpty(responseModel.AccessToken))
                {
                    return responseModel;
                }
                else
                {
                    throw new ApiException("获取LWA Access_Token异常");
                }
            }
        }

    }
}
