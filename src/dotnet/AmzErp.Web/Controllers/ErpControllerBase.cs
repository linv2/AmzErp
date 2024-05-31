using AmzErp.Common.Mvc.Controller;
using Microsoft.AspNetCore.Mvc;

namespace AmzErp.Web.Controllers
{
    [Route("erp/[controller]/[action]")]
    public class ErpControllerBase : BaseController<ErpControllerBase>
    {
    }
}
