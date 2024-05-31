using AmazonSDK.Models;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace AmazonSDK.Attribute
{
    internal static class APIResourceHelper
    {
        private static ConcurrentDictionary<string, APIResourceAttribute> TypeDictionary { get; }
        static APIResourceHelper()
        {
            TypeDictionary = new ConcurrentDictionary<string, APIResourceAttribute>();
        }
        internal static APIResourceAttribute GetAPIResource<TSource>(this TSource model) where TSource : AmazonRequestModel
        {
            if (model == null)
            {
                throw new NullReferenceException();
            }

            var modelType = model.GetType();
            var requestType = GetAPIResource(modelType);
            return requestType;
        }

        private static APIResourceAttribute GetAPIResource(Type type)
        {
            var typeName = type.FullName;
            if (!TypeDictionary.TryGetValue(typeName, out var resultValue))
            {
                var requestMethodAttribute = type.GetCustomAttributes<APIResourceAttribute>().FirstOrDefault();
                if (requestMethodAttribute == null || string.IsNullOrEmpty(requestMethodAttribute.ResourceUri))
                {
                    throw new System.Exception($"{typeName}RequestMethodAttribute");

                }

                resultValue = requestMethodAttribute;
                TypeDictionary.TryAdd(typeName, resultValue);
            }

            return resultValue;
        }
    }
}
