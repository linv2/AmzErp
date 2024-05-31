using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace AmzErp.Common.Mvc.ModelBinder.Json.Net
{
    public class JObjectModelBinder : IModelBinder
    {
        public JObjectModelBinder(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
        }
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException("bindingContext");
            var context = bindingContext.ActionContext.HttpContext;
            try
            {
                if (bindingContext.ModelType == typeof(JObject))
                {
                    if (bindingContext.BindingSource == BindingSource.Body)
                    {
                        using var streamReader = new StreamReader(context.Request.Body);
                        using var jsonTextReader = new JsonTextReader(streamReader);
                        var jObject =await JObject.LoadAsync(jsonTextReader);
                        bindingContext.Result = ModelBindingResult.Success(jObject);
                    }
                    else if (bindingContext.BindingSource == BindingSource.Form)
                    {
                        var jObject = new JObject();
                        foreach (var item in bindingContext.ActionContext.HttpContext.Request.Form)
                        {
                            jObject.Add(new JProperty(item.Key, item.Value));
                        }
                        bindingContext.Result = (ModelBindingResult.Success(jObject));
                    }
                    else if (bindingContext.BindingSource == BindingSource.Query)
                    {
                        JObject jObject = new JObject();
                        foreach (var item in bindingContext.ActionContext.HttpContext.Request.Query)
                        {
                            jObject.Add(new JProperty(item.Key, item.Value));
                        }
                        bindingContext.Result = (ModelBindingResult.Success(jObject));
                    }
                    else
                    {
                        bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "不支持的参数特性");
                    }
                }
            }
            catch (System.Exception exception)
            {
                if (!(exception is FormatException) && (exception.InnerException != null))
                {
                    exception = ExceptionDispatchInfo.Capture(exception.InnerException).SourceException;
                }
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, exception, bindingContext.ModelMetadata);
            }
        }
    }
}