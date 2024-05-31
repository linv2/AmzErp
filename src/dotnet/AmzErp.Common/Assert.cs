using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using AmzErp.Common.Exception;

namespace AmzErp.Common
{

    public class Assert
    {
        public static void IsNull(object value, string message = "对象不能为空")
        {

            if (value == null)
            {
                throw new ApiException(message);
            }
        }

        public static void IsNumber(string value, string message = "对象格式错误")
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ApiException(message);
            }
            Regex(value, @"^\d+$", message);
        }

        public static void MaxLength(string value, int maxLength = 50, string message = "数据内容超长")
        {
            if ((value?.Length ?? 0) > maxLength)
            {
                throw new ApiException(message);
            }
        }

        public static void MinLength(string value, int minLength = 0, string message = "数据内容过短")
        {
            if ((value?.Length ?? 0) < minLength)
            {
                throw new ApiException(message);
            }
        }


        public static void RangeLength(string value, int minLength = 0, int maxLength = 50, string message = "数据长度错误")
        {
            var length = value?.Length ?? 0;
            if (length < minLength || length > maxLength)
            {
                throw new ApiException(message);
            }
        }

        public static void IsNullOrEmpty(string value, string message = "值不能为空")
        {

            if (string.IsNullOrEmpty(value))
            {
                throw new ApiException(message);
            }
        }

        public static void Regex(string value, string pattern, string message = "输入格式错误")
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(value, pattern))
                    {
                        throw new ApiException(message);
                    }
                }
                catch (RegexMatchTimeoutException)
                {
                    throw new ApiException($"正则格式错误：{pattern}");
                    // ignored
                }
            }
        }
        public static void IsNotNull(object value, string message = "对象不能为空")
        {
            if (value != null)
            {
                throw new ApiException(message);
            }
        }
        public static void IsTrue(bool value, string message = null)
        {
            if (value)
            {
                throw new ApiException(message);
            }
        }
        public static void IsFalse(bool value, string message = null)
        {
            if (!value)
            {
                throw new ApiException(message);
            }
        }

        public static void IsEquals<T>(T value1, T value2, string message = "参数异常")
        {
            IsNull(value1);
            IsNull(value2);
            if (value1.Equals(value2))
            {
                throw new ApiException(message);
            }
        }
        public static void IsNotEquals<T>(T value1, T value2, string message = "参数异常")
        {
            IsNull(value1);
            IsNull(value2);
            if (!value1.Equals(value2))
            {
                throw new ApiException(message);
            }
        }
    }

    public static class AssertExpand
    {
        public static void IsNull(this object value, string message = "对象不能为空")
        {
            Assert.IsNull(value, message);
        }
        public static string IsNullOrEmpty(this string value, string message = "对象不能为空")
        {
            Assert.IsNullOrEmpty(value, message);
            return value;
        }
        public static int IsNumber(this string value,string message = "对象格式错误")
        {
            Assert.IsNumber(value, message);
            return Convert.ToInt32(value);
        }
        public static string MaxLength(this string value, int maxLength = 50, string message = "数据内容超长")
        {
            Assert.MaxLength(value, maxLength, message);
            return value;
        }

        public static string MinLength(this string value, int minLength = 0, string message = "数据内容过短")
        {

            Assert.MinLength(value, minLength, message);
            return value;
        }


        public static string RangeLength(this string value, int minLength = 0, int maxLength = 50, string message = "数据长度错误")
        {
            RangeLength(value, minLength, maxLength, message);
            return value;
        }

    }
}
