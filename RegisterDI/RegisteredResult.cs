using System;
using Microsoft.Extensions.DependencyInjection;

namespace RegisterDI
{
    public class RegisteredResult
    {
        public Type Class { get; }
        public Type Interface { get; }
        public ServiceLifetime Lifetime { get; }

        public RegisteredResult(Type classType, Type interfaceType, ServiceLifetime lifetime)
        {
            Class = classType;
            Interface = interfaceType;
            Lifetime = lifetime;
        }

        public override string ToString()
        {
            return Class == null 
                ? $"The interface {Interface.Name} is ignored" 
                : $"{Class.Name} : {Interface.Name} ({Lifetime})";
        }
    }
}