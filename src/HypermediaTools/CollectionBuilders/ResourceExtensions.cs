using System;
using FubuCore;

namespace HypermediaTools.CollectionBuilders
{
    public static class ResourceExtensions
    {
        public static string GetResourceName(this Type resource_type)
        {
            var resourceAttribute = resource_type.GetCustomAttributes(typeof (ResourceAttribute), false);
            if(resourceAttribute.Length > 0)
            {
                return ((ResourceAttribute) resourceAttribute[0]).Name;
            }
            return resource_type.Name;
        }
        public static string GetResourceHref(this IAmAResource resource)
        {
            return "/{0}/{1}".ToFormat(resource.GetResourceName(), resource.GetIdentifer());
        }
    }
}