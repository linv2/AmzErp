using AmzErp.Common.Attribute;
using AmzErp.Common.Attribute.Service;
using AmzErp.Common.Result;
using AmzErp.DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AmzErp.Common.Mvc.Controller
{
    [Route("[controller]/[action]")]
    [ApiController]
    public abstract class BaseController<T> : ControllerBase where T : BaseController<T>
    {
        [Autowired]
        public ILogger<T> Logger { get; set; }
        [Autowired]
        public AmzErp.DataAccess.AmzErpDbContext DbContext { get; set; }






        #region 返回/输出

        protected IActionResult OkResult => Ok(ResultData.Ok);

        protected IActionResult SuccessMessage(string message, int code = 200)
        {
            return Ok(ResultData.Success(message, code));
        }

        protected IActionResult Success<TParam>(TParam data)
        {
            return Ok(ResultData.Data(data, "操作成功", 200));
        }
        protected IActionResult Data<TParam>(TParam data)
        {
            return Ok(ResultData.Data(data, "操作失败", 400));
        }

        protected IActionResult Fail(string message, int code = 400)
        {
            return Ok(ResultData.Fail(message, code));
        }

        protected IActionResult SuccessList<TParam>(IEnumerable<TParam> enumerable, int total = 0)
        {
            return Ok(ResultData.Success(enumerable, total));
        }
        protected IActionResult SuccessList<TParam>(IOrderedQueryable<TParam> queryable, int pageIndex, int pageSize = 50)
        {
            pageIndex--;
            pageIndex = pageIndex < 0 ? 0 : pageIndex;
            var data = queryable.Skip(pageIndex * pageSize).Take(pageSize);
            return Ok(ResultData.Success(data, queryable.Count()));
        }
        protected IActionResult SuccessList<TParam,TResult>(IOrderedQueryable<TParam> queryable, int pageIndex=0, int pageSize = 50, Func<TParam, TResult> expression=null)
        {
            pageIndex--;
            pageIndex = pageIndex < 0 ? 0 : pageIndex;
            var data = queryable.Skip(pageIndex * pageSize).Take(pageSize).AsEnumerable().Select(x => new
            {
                item = x,
                ext = expression(x)
            });
            return Ok(ResultData.Success(data, queryable.Count()));
        }

        #endregion 返回/输出

        public User UserInfo => (User)HttpContext?.Items["user"];

    }
}