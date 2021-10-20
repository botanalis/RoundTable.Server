using System;
using Microsoft.Extensions.DependencyInjection;

namespace RegisterDI.Attributes
{
    /// <summary>
    /// Object DI Life Time
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterWithLifetimeAttribute : Attribute
    {
        public ServiceLifetime RequiredLifeTime { get; }

        public RegisterWithLifetimeAttribute(ServiceLifetime requiredLifeTime)
        {
            RequiredLifeTime = requiredLifeTime;
        }
    }
}