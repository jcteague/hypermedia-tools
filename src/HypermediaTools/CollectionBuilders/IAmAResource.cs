using FubuCore.Reflection;
using System.Linq;

namespace HypermediaTools.CollectionBuilders
{
    public abstract class IAmAResource
    {
        public abstract string GetIdentifer();
        
        public virtual string GetResourceName()
        {
            var resource_attribute = this.GetType().GetCustomAttributes(typeof (ResourceAttribute),false).FirstOrDefault();
            if (resource_attribute != null) return ((ResourceAttribute) resource_attribute).Name;
            return this.GetType().Name;
        }
    }
}