using AmazonSDK;
using AmazonSDK.LWA;
using AmazonSDK.StaticConfigResourse;
using AmzErp.Common;
using AmzErp.Common.Model;
using AmzErp.Common.Mvc.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Z.EntityFramework.Plus;

namespace AmzErp.Web.Controllers
{
    public class AmazonController : BaseController<AmazonController>
    {
        public AmzDeveloperConfig amzDeveloperConfig { get; set; }

        private ILWAClient LWAClient { get; set; }

        public AmazonController(AmzDeveloperConfig amzDeveloperConfig)
        {
            this.amzDeveloperConfig = amzDeveloperConfig;
            LWAClient = new DefaultLWAClient(amzDeveloperConfig.ClientId, amzDeveloperConfig.ClientSecret);
        }
        [HttpGet("{hashKey}"),AllowAnonymous]
        public IActionResult OAuth(string hashKey, bool beta = true)
        {

            var redirectUrl = LWAClient.GetAuthorizationUrl(amzDeveloperConfig.ApplicationId, hashKey, beta);
            return Redirect(redirectUrl);
        }

        /// <summary>
        /// 认证成功回调
        /// </summary>
        /// <param name="spapi_oauth_code"></param>
        /// <param name="state"></param>
        /// <param name="selling_partner_id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult CallBack(string spapi_oauth_code, string state, string selling_partner_id)
        {
            var shopId = DbContext.AmzShop.Where(x => x.HashKey.Equals(state) && x.Selling_Partner_Id == null).FirstOrDefault()?.Id ?? 0;
            Assert.IsTrue(shopId == 0, "操作成功，请关闭页面，刷新列表查看");
            var responseModel = LWAClient.AccessToken(spapi_oauth_code);
            if (responseModel != null)
            {
                DbContext.AmzShop.Where(x => x.Id == shopId).Update(x => new DataAccess.Entity.AmzShop()
                {

                    RefreshToken = responseModel.RefreshToken,
                    Selling_Partner_Id = selling_partner_id,
                    AuthorizationTime = DateTime.Now
                });
                DbContext.SaveChanges();
            }
            return Content("操作成功，请关闭页面，刷新列表查看");
        }
        public IActionResult refreshToken()
        {
            var responseModel = LWAClient.RefreshToken("Atzr|IwEBIMkOkzND3Ew4rTUV5Q227Wd_cK99wQxkz4ZiQcKUbGf2EZQGqMSEdB9iUkOYUSa6Usks5KYzwT0pmY7XqtAW-s3p67dMhjR1Bst5SHUku2xEPBR0B1HivJml2436WOHs-ziT-AAIh6VZvA6iTjcY6_03ch1a5dXyj7lJq8xKqj1wRkXj0a_3GWGiywqTdSapBYSG2Fl4yIqQam_tHOA54ELenW9QXRvf-wDq7IrjTJUKHmfL8NwMhsHufFbGICd7neI-08-6Y1mwAfwQPhRIYkoJs40lHeY7gyw3KjVbreT4EKfruVIli04N_u5zPaLfkq4");
            return Ok(responseModel);
        }

        public IActionResult Test()
        {
            var awsClient = AWSClient.Create(new AWSAuthenticationCredentials()
            {
                AccessKeyId = amzDeveloperConfig.AccessKey,
                SecretKey = amzDeveloperConfig.SecretKey,
                SellerRegion = SellerRegion.SANDBOX_NORTH_AMERICA
            });
            awsClient.DebugEvent += AwsClient_DebugEvent;
            var responseModel = LWAClient.RefreshToken("Atzr|IwEBIMkOkzND3Ew4rTUV5Q227Wd_cK99wQxkz4ZiQcKUbGf2EZQGqMSEdB9iUkOYUSa6Usks5KYzwT0pmY7XqtAW-s3p67dMhjR1Bst5SHUku2xEPBR0B1HivJml2436WOHs-ziT-AAIh6VZvA6iTjcY6_03ch1a5dXyj7lJq8xKqj1wRkXj0a_3GWGiywqTdSapBYSG2Fl4yIqQam_tHOA54ELenW9QXRvf-wDq7IrjTJUKHmfL8NwMhsHufFbGICd7neI-08-6Y1mwAfwQPhRIYkoJs40lHeY7gyw3KjVbreT4EKfruVIli04N_u5zPaLfkq4");

            var response = awsClient.Request(new AmazonSDK.Models.Request.Sellers.GetMarketplaceParticipationsRequest(), responseModel.AccessToken);
            return Ok(response);
        }

        private void AwsClient_DebugEvent(string message)
        {
            Logger.LogDebug(message);
        }

        public IActionResult Test1()
        {
            var awsClient = AWSClient.Create(new AWSAuthenticationCredentials()
            {
                AccessKeyId = amzDeveloperConfig.AccessKey,
                SecretKey = amzDeveloperConfig.SecretKey,
                SellerRegion = SellerRegion.SANDBOX_NORTH_AMERICA
            });
            awsClient.DebugEvent += AwsClient_DebugEvent;
            var responseModel = LWAClient.RefreshToken("Atzr|IwEBIMkOkzND3Ew4rTUV5Q227Wd_cK99wQxkz4ZiQcKUbGf2EZQGqMSEdB9iUkOYUSa6Usks5KYzwT0pmY7XqtAW-s3p67dMhjR1Bst5SHUku2xEPBR0B1HivJml2436WOHs-ziT-AAIh6VZvA6iTjcY6_03ch1a5dXyj7lJq8xKqj1wRkXj0a_3GWGiywqTdSapBYSG2Fl4yIqQam_tHOA54ELenW9QXRvf-wDq7IrjTJUKHmfL8NwMhsHufFbGICd7neI-08-6Y1mwAfwQPhRIYkoJs40lHeY7gyw3KjVbreT4EKfruVIli04N_u5zPaLfkq4");


            var response = awsClient.Request(new AmazonSDK.Models.Request.Feeds.CreateFeedDocumentSpecificationRequest()
            {
                ContentType = "text/tab-separated-values; charset=UTF-8"
            }, responseModel.AccessToken);



            return Ok(response);
        }

        public IActionResult UpdateFile()
        {
            string USD = "USD";
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>");
            sb.Append("<AmazonEnvelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:noNamespaceSchemaLocation=\"amzn-envelope.xsd\">");
            sb.Append("<Header>");
            sb.Append("<DocumentVersion>1.01</DocumentVersion>");//版本
            sb.Append("<MerchantIdentifier>M_EXAMPLE_123456</MerchantIdentifier>");
            sb.Append("</Header>");
            sb.Append("<MessageType>Product</MessageType>");
            sb.Append("<PurgeAndReplace>false</PurgeAndReplace>");
            sb.Append("<Message>");
            sb.Append("<MessageID>1</MessageID>");
            sb.Append("<OperationType>Create</OperationType>");
            #region 产品            
            sb.Append("<Product>");
            sb.Append("<SKU>121212</SKU>");//产品sku
            sb.Append("<StandardProductID>");//标准产品ID
            sb.Append("<Type>ASIN</Type>");//产品ASIN
            sb.Append("<Value>B06ZYX7X29</Value>");//产品ASIN值
            sb.Append("</StandardProductID>");
            sb.Append("<ProductTaxCode>154834566</ProductTaxCode>");//产品税代码
            #region 产品描述
            sb.Append("<DescriptionData>");
            sb.Append("<Title>This is a book</Title>");//产品标题-不为空
            sb.Append("<Brand>ehuifheu</Brand>");//产品品牌-
            sb.Append("<Description>This is a book but you know</Description>");//产品描述-不为空
            sb.Append("<BulletPoint>wqdqwd B</BulletPoint>");//产品特性简介。-不为空
            sb.Append("<BulletPoint>fefew B</BulletPoint>");//产品特性简介。
            sb.Append("<MSRP currency='" + USD + "'>25.19</MSRP>");//产品零售价
            sb.Append("<Manufacturer>dqwdwqd Pr</Manufacturer>");//制造商
            sb.Append("<ItemType>example-item-typeefew</ItemType>");//产品类型
            sb.Append("<ItemDimensions>fewfewfv</ItemDimensions>");//产品大小
            sb.Append("<PackageDimensions>feewf</PackageDimensions>");//包的尺寸
            sb.Append("<PackageWeight>wefwe</PackageWeight>");//包裹重量
            sb.Append("<ShippingWeight>ttre</ShippingWeight>");//运输重量
            sb.Append("<MerchantCatalogNumber>fewf</MerchantCatalogNumber>");//商业产品目录号-不为空
            sb.Append("<MaxOrderQuantity>efwwe</MaxOrderQuantity>");//最大订单数量
            sb.Append("<SerialNumberRequired>wefewf</SerialNumberRequired>");//所需的序列号
            sb.Append("<Prop65>grehtrh</Prop65>");//指示产品须受加州65号提案规管。
            sb.Append("<LegalDisclaimer>grerwe</LegalDisclaimer>");//法律免责声明
            sb.Append("<MfrPartNumber>grrg</MfrPartNumber>");//生产商零件号
            sb.Append("<SearchTerms>reger</SearchTerms>");//检索项目
            sb.Append("<PlatinumKeywords>wefewf</PlatinumKeywords>");//产品映射到自定义浏览节点结构中的节点的值
            sb.Append("<Memorabilia>fewew</Memorabilia>");//产品是纪念品。
            sb.Append("<Autographed>rgeerg</Autographed>");//产品是已签名的项目
            sb.Append("<UsedFor>gergre</UsedFor>");//产品的用途。影响产品在浏览节点结构中的位置
            sb.Append("<OtherItemAttributes>ewrgfrewgre</OtherItemAttributes>");//其他项目属性
            sb.Append("<TargetAudience>grregre</TargetAudience>");//目标客户
            sb.Append("<SubjectContent>efwefgw</SubjectContent>");//主题内容
            sb.Append("<IsGiftWrapAvailable>ergreg</IsGiftWrapAvailable>");//可以用礼品包装吗
            sb.Append("<IsGiftMessageAvailable>ergerg</IsGiftMessageAvailable>");//有礼品信息
            sb.Append("<IsDiscontinuedByManufacturer>efwwef</IsDiscontinuedByManufacturer>");//被制造商停止生产
            sb.Append("<MaxAggregateShipQuantity>wefwefw</MaxAggregateShipQuantity>");//最大船舶总数量
            sb.Append("<Variation VariationTheme='size'></Variation>");
            sb.Append("</DescriptionData>");//产品描述
            #endregion
            #region 产品数据
            sb.Append("<ProductData>");
            sb.Append("<Health>");
            sb.Append("<ProductType>");
            sb.Append("<HealthMisc>");
            sb.Append("<Ingredients>fgewrg</Ingredients>");
            sb.Append("<Directions>fewewf</Directions>");
            sb.Append("</HealthMisc>");
            sb.Append("</ProductType>");
            sb.Append("</Health>");
            sb.Append("</ProductData>");
            #endregion
            sb.Append("</Product>");
            #endregion
            sb.Append("</Message>");
            sb.Append("</AmazonEnvelope>");

            var url = "https://d34o8swod1owfl.cloudfront.net/Feed_101__POST_PRODUCT_DATA_%2BKEY%3DFeed_101%2BMode%3DCBC%2BINITVEC%3D8f+6c+cc+56+0d+50+a2+d0+31+ec+80+be+f2+6a+1d+0a";
            var restClient = new RestClient();
            var request = new RestRequest(url, Method.POST);
            request.AddHeader("Content-Type", "text/plain; charset=utf-8");
            request.AddJsonBody(aes(sb.ToString()));
            var response = restClient.Execute(request);
            return Ok(response.Content);
        }
        private byte[] aes(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var aes = Aes.Create();
            aes.GenerateIV();
            aes.GenerateKey();
            byte[] resultArray = aes.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            return resultArray;
        }
        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

    }
}
