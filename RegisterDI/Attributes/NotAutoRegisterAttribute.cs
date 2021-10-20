using System;

namespace RegisterDI.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NotAutoRegisterAttribute: Attribute
    {
        
    }
}