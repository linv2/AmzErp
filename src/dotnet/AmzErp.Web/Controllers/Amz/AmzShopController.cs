using AmzErp.Common;
using AmzErp.Common.Attribute;
using AmzErp.Common.Mvc.Controller;
using AmzErp.Common.Mvc.Filters;
using AmzErp.DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace AmzErp.Web.Controllers
{
    public class AmzShopController : BaseController<AmazonController>
    {
        [HttpPost, ModelValidation]
        public IActionResult Add(AmzShop amzShop)
        {
            amzShop.UserId = UserInfo.Id;
            amzShop.CreatedTime = DateTime.Now;
            DbContext.AmzShop.Add(amzShop);
            DbContext.SaveChanges();
            return OkResult;
        }
        public IActionResult Endable(int id, bool status)
        {
            DbContext.AmzShop.Where(x => x.Id.Equals(id) && x.UserId == UserInfo.Id).Update(x => new AmzShop()
            {
                Endable = status
            });
            DbContext.SaveChanges();
            return OkResult;
        }
        public IActionResult List(int pageIndex = 0, int pageSize = 10, string name = null)
        {
            pageIndex--;
            pageIndex = pageIndex < 0 ? 0 : pageIndex;
            var query = DbContext.AmzShop.Include(x => x.User).AsNoTracking();
            if (UserInfo.UserType == DataAccess.Enum.UserType.User)
            {
                query = query.Where(x => x.UserId == UserInfo.Id);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }
            var data = query.OrderByDescending(x => x.Id).Skip(pageIndex * pageSize).Take(pageSize);
            return SuccessList(data, query.Count());

        }
        public IActionResult OAuth(int id)
        {
            var query = DbContext.AmzShop.Where(x => x.Id.Equals(id) && x.Selling_Partner_Id == null);
            if (UserInfo.UserType == DataAccess.Enum.UserType.User)
            {
                query = query.Where(x => x.UserId == UserInfo.Id);
            }

            Assert.IsFalse(query.Any(), "状态错误，请返回刷新页面重试");
            var hashKey = Guid.NewGuid().ToString("N");
            query.Update(x => new AmzShop
            {
                HashKey = hashKey
            }); ;
            DbContext.SaveChanges();
            return Success(hashKey);

        }
        public Task<IActionResult> RefreshShop(int id)
        {
            var query = DbContext.AmzShop.Where(x => x.Id.Equals(id) && x.Selling_Partner_Id != null);
            if (UserInfo.UserType == DataAccess.Enum.UserType.User)
            {
                query = query.Where(x => x.UserId == UserInfo.Id);
            }
            var amzShop = query.FirstOrDefault();
            if (amzShop == null)
            {
                return Task.FromResult(Fail("状态错误，请返回刷新页面重试"));
            }
            return Task.FromResult(OkResult);
        }

        public IActionResult ListMarketplace(int id)
        {
            var query = DbContext.AmzMarketplace.Where(x => x.Id.Equals(id));
            if (UserInfo.UserType == DataAccess.Enum.UserType.User)
            {
                query = query.Where(x => x.UserId == UserInfo.Id);
            }
            var list = query.ToList();
            return SuccessList(list, list.Count);
        }
    }
}
