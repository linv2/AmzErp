using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmzErp.Common;
using AmzErp.Common.Mvc.Controller;
using AmzErp.Common.Mvc.Filters;
using AmzErp.DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Z.EntityFramework.Plus;

namespace AmzErp.Web.Controllers.Erp
{
    public class CategoryController : ErpControllerBase
    {
        public IActionResult List(string name = null, int? parentId = null, int pageIndex = 0, int pageSise = 99)
        {
            var erpCategory = DbContext.ErpCategory.Where(x=>!x.Delete).AsNoTracking();
            if (!string.IsNullOrEmpty(name))
            {
                erpCategory = erpCategory.Where(x => x.Name.Contains(name));
            }
            if (parentId.HasValue)
            {
                erpCategory = erpCategory.Where(x => x.ParentId.Equals(parentId.Value));
            }
            if (UserInfo.UserType == DataAccess.Enum.UserType.User)
            {
                erpCategory = erpCategory.Where(x => x.UserId.Equals(UserInfo.Id));
            }
            return SuccessList(erpCategory.OrderBy(x => x.Id), pageIndex, pageSise);
        }

        [HttpPost]
        public IActionResult Add([FromBody] JObject jObject)
        {
            var name = jObject["name"]?.Value<string>()
                 .IsNullOrEmpty()
                 .RangeLength(1, 100, "名字长度错误");
            var parentId = jObject["parentId"].Value<int>();
            var erpCategory = new ErpCategory()
            {
                Name = name,
                ParentId = parentId,
                CreatedTime = DateTime.Now,
                Delete = false,
                IsEndable = true,
                UserId = UserInfo.Id,
                ChildCount = 0,
                Depth = 0,
                Tree = ""
            };
            if (parentId > 0)
            {
                var pCategory = DbContext.ErpCategory.FirstOrDefault(x => x.Id.Equals(parentId) && x.UserId.Equals(UserInfo.Id));
                pCategory.IsNull("系统异常，请稍后再试");
                erpCategory.Depth = pCategory.Depth + 1;
                erpCategory.Tree = string.IsNullOrEmpty(pCategory.Tree) ? pCategory.Id.ToString() : $"{pCategory.Tree}_{pCategory.Id}";

            }
            DbContext.ErpCategory.Add(erpCategory);
            DbContext.SaveChanges();
            if (parentId > 0)
                DbContext.ErpCategory.Where(x => x.Id.Equals(parentId) && x.UserId.Equals(UserInfo.Id)).Update(x => new ErpCategory()
                {
                    ChildCount = x.ChildCount + 1
                });
            return OkResult;
        }

        [HttpPost]
        public IActionResult Edit([FromBody] JObject jObject)
        {
            var name = jObject["name"]?.Value<string>()
                    .IsNullOrEmpty()
                    .RangeLength(1, 100, "名字长度错误");
            var id = jObject["id"]?.Value<string>().IsNullOrEmpty().IsNumber();
            DbContext.ErpCategory.Where(x => x.Id.Equals(id) && x.UserId.Equals(UserInfo.Id) && !x.Delete).Update(x => new ErpCategory()
            {
                Name = name
            });
            return OkResult;
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            DbContext.ErpCategory.Where(x => x.Id.Equals(id) && !x.Delete && x.UserId.Equals(UserInfo.Id)).Update(x => new ErpCategory()
            {
                Delete = true
            });
            return OkResult;
        }
        [HttpGet]
        public IActionResult Disable(int id)
        {
            DbContext.ErpCategory.Where(x => x.Id.Equals(id) && !x.Delete && x.UserId.Equals(UserInfo.Id)).Update(x => new ErpCategory()
            {
                IsEndable = !x.IsEndable
            }); 
            return OkResult;
        }
    }
}