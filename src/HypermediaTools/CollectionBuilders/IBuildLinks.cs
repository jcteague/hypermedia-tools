using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HypermediaTools.Attributes;
using HypermediaTools.Models;
using FubuCore;

namespace HypermediaTools.CollectionBuilders
{
    public interface IBuildLinks<T> where T: IAmAResource
    {
        IEnumerable<Link> GetLinks(T resource, bool include_embedded_resource_links = false);
    }

    public class LinkBuilder<T>: IBuildLinks<T> where T : IAmAResource
    {
        public IEnumerable<Link> GetLinks(T resource, bool include_embedded_resource_links = false)
        {

            var links = new List<Link>();
            foreach (var resource_property in GetRelatedResourceProperties(resource, include_embedded_resource_links))
            {
                links.Add(create_resource_link(resource,resource_property));
            }
            foreach (var collection_property in GetEnumeratedResources(resource, include_embedded_resource_links))
            {
                links.Add(create_collection_link(resource,collection_property));
            }
            return links;
        }

        Link create_collection_link(T resource, PropertyInfo collectionProperty)
        {
            var collection_resource_type = collectionProperty.PropertyType.GetGenericArguments()[0];
            var resource_name = collection_resource_type.GetResourceName();
            var href = string.Format("/{0}/{1}/{2}", 
                                      resource.GetResourceName(), 
                                      resource.GetIdentifer(),
                                      resource_name);
            return new Link
                       {
                           href = href,
                           name = collectionProperty.Name,
                           rel = collectionProperty.PropertyType.GetGenericArguments()[0].Name,
                           prompt = collectionProperty.Name
                       };
        }

        Link create_resource_link(T resource, PropertyInfo resourceProperty)
        {
            var linked_resource = (IAmAResource)resourceProperty.GetValue(resource, null);
            var href = string.Format("/{0}/{1}", linked_resource.GetResourceName(), linked_resource.GetIdentifer());
            return new Link
                       {
                           href = href,
                           name = resourceProperty.Name,
                           rel = resourceProperty.PropertyType.Name,
                           prompt = resourceProperty.Name
                       };
        }

        IEnumerable<PropertyInfo> GetRelatedResourceProperties(T resource, bool includeEmbeddedResourceLinks)
        {
            foreach (var prop in typeof (T).GetProperties().Where(p=>p.PropertyType.BaseType == typeof(IAmAResource)))
            {
                var is_embedded = prop.GetCustomAttributes(typeof (EmbeddedResourceAttribute), true).Count() > 0;
                if(!is_embedded || includeEmbeddedResourceLinks)
                    yield return prop;
            }
        }

        IEnumerable<PropertyInfo> GetEnumeratedResources(T resource, bool includeEmbeddedResourceLinks)
        {
            var property_type = typeof (T);
            foreach(var prop in typeof (T).GetProperties().Where(enumerable_of_resource))
            {
                var is_embedded = prop.GetCustomAttributes(typeof(EmbeddedResourceAttribute), true).Count() > 0;
                if (!is_embedded || includeEmbeddedResourceLinks)
                    yield return prop;
            }
                
        }

        static bool enumerable_of_resource(PropertyInfo prop)
        {
            var genericArguments = prop.PropertyType.GetGenericArguments();
            if(genericArguments.Length > 0)
                return genericArguments[0].BaseType == typeof(IAmAResource);
            return false;
        }
    }
    
}