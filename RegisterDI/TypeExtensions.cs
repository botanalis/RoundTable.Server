using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RegisterDI.Attributes;

namespace RegisterDI
{
    public static class TypeExtensions
    {
        /// <summary>
        /// 忽略該 Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsIgnoredType(this Type type)
        {
            return type.GetCustomAttribute<NotAutoRegisterAttribute>() != null;
        }
        
        /// <summary>
        /// 多個 Attribute Lift time 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsMultipleLifeTime(this Type type)
        {
            return type.GetCustomAttributes(typeof(RegisterWithLifetimeAttribute), true).Length > 1;
        }
        

        /// <summary>
        /// 取得 Attribute Set ServiceLifeTime
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ServiceLifetime? GetLifeTimeForClass(this Type type)
        {
            return type.GetCustomAttribute<RegisterWithLifetimeAttribute>(true)?.RequiredLifeTime;
        }
        
    }
}