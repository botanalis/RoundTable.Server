using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace RegisterDI
{
    public static class RegisterHelper
    {
        /// <summary>
        /// 註冊通用
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static RegisterData RegisterAssemblyPublicGenericClass(this IServiceCollection services,
            params Assembly[] assemblies)
        {
            if (assemblies.Length == 0)
            {
                assemblies = new[] {Assembly.GetCallingAssembly()};
            }

            var types = assemblies
                .SelectMany( x => 
                    x.GetExportedTypes().Where(item => 
                        item.IsClass && 
                        !item.IsAbstract && 
                        !item.IsGenericType && 
                        !item.IsNested && 
                        !item.IsIgnoredType()));

            return new RegisterData(services, types);

        }

        /// <summary>
        /// 過濾條件
        /// </summary>
        /// <param name="regData"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static RegisterData Where(this RegisterData regData, Func<Type, bool> predicate)
        {
            if (regData == null)
            {
                throw new ArgumentNullException(nameof(regData));
            }

            return new RegisterData(regData.Services, regData.TypesToConsider.Where(predicate));
        }

        /// <summary>
        /// 忽略的 Interface
        /// </summary>
        /// <param name="regData"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static RegisterData IgnoreInterface<T>(this RegisterData regData)
        {
            if (!typeof(T).IsInterface)
            {
                throw new InvalidOperationException($"This provided {typeof(T).Name} is not interface");
            }
            regData.InterfacesToIgnore.Add(typeof(T));

            return regData;
        }

        /// <summary>
        /// 註冊實作
        /// </summary>
        /// <param name="regData"></param>
        /// <param name="lifeTime"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IList<RegisteredResult> AsPublicImplementedInterfaces(this RegisterData regData,
            ServiceLifetime lifeTime = ServiceLifetime.Transient)
        {
            if (regData == null)
            {
                throw new ArgumentNullException(nameof(regData));
            }

            var result =
                regData.InterfacesToIgnore
                    .Select(x => new RegisteredResult(null, x, ServiceLifetime.Singleton))
                    .ToList();

            foreach (var classType in regData.TypesToConsider)
            {
                if (classType.IsMultipleLifeTime())
                {
                    throw new ArgumentException($"Class {classType.FullName} has multiple life time attributes");
                }

                // 取得 class 實作 interface
                var interfaces = classType.GetTypeInfo().ImplementedInterfaces;
                
                var effInterfaces
                    = interfaces.Where(x =>
                    !regData.InterfacesToIgnore.Contains(x) &&
                    x.IsPublic &&
                    !x.IsNested);

                foreach (var infc in effInterfaces)
                {
                    var someReg = result.FirstOrDefault(x => x.Class != null && x.Interface.FullName == infc.FullName); 
                    if (someReg != null)
                    {
                        throw new ArgumentException($"Interface {infc.FullName} implement, Class choose one between {someReg.Class.FullName} and {classType.FullName}");
                    }
                    
                    var lifetTimeForClass = classType.GetLifeTimeForClass() ?? lifeTime;
                    regData.Services.Add(new ServiceDescriptor(infc, classType, lifetTimeForClass));
                    result.Add(new RegisteredResult(classType, infc, lifetTimeForClass));
                }
            }

            return result;
        }
    }
}