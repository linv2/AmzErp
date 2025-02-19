﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmzErp.Common.Mvc.ModelBinder.Json.Net
{
    public class JObjectModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (context.Metadata.ModelType == (typeof(JObject)))
            {
                return new JObjectModelBinder(context.Metadata.ModelType);
            }
            else if (context.Metadata.ModelType == (typeof(JToken)))
            {
                return new JObjectModelBinder(context.Metadata.ModelType);
            }
            return null;
        }
    }
}
