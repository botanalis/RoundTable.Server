using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace RegisterDI
{
    public class RegisterData
    {
        public IEnumerable<Type> TypesToConsider { get; set; }

        public IServiceCollection Services { get; set; }
        
        public List<Type> InterfacesToIgnore { get; set; }
        
        public RegisterData(IServiceCollection services, IEnumerable<Type> typesToConsider)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            TypesToConsider = typesToConsider ?? throw new ArgumentNullException(nameof(typesToConsider));

            this.InterfacesToIgnore = new List<Type>
            {
                typeof(IDisposable),
                typeof(ISerializable)
            };
        }

    }
}