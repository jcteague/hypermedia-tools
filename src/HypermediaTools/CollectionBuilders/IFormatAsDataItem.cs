using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HypermediaTools.Attributes;
using HypermediaTools.Models;
using HypermediaTools.Serialization;
using FubuCore;

namespace HypermediaTools.CollectionBuilders
{
    public interface IFormatAsDataItem<T>
    {
        IEnumerable<Data> AsDataItem(object entity);
        IEnumerable<Data> FormatType(Type object_type);
    }

    public class DefaultDataItemFormatter<T> : IFormatAsDataItem<T>
    {
        IFieldSerializer serializer;

        public DefaultDataItemFormatter(IFieldSerializer serializer)
        {
            this.serializer = serializer;
        }

        public IEnumerable<Data> AsDataItem(object entity)
        {
            var entity_type = entity.GetType();
            var entity_properties = entity_type.GetProperties();
            var result = new List<Data>();
          
            foreach (var property in entity_properties)
            {
                if(should_format_as_data_item(property))
                {
                    
                    var data_item = CreateTemplate(property);
                    data_item.value = serializer.Serialize(property.GetValue(entity, null));
                    result.Add(data_item);    
                    
                    
                }
            }
            return result;
        }

        public IEnumerable<Data> FormatType(Type object_type)
        {
            var entity_properties = object_type.GetProperties();
            var result = new List<Data>();
          
            foreach (var property in entity_properties)
            {
                if(should_format_as_data_item(property))
                {
                    
                    var data_item = CreateTemplate(property);
                    result.Add(data_item);    
                    
                    
                }
            }
            return result;
        }

        bool should_format_as_data_item(PropertyInfo property)
        {
            var t = property.PropertyType;
            var is_representable = t.BaseType == typeof (IAmAResource) || t.IsGenericEnumerable();

            if (is_representable) return false;

            return true;


        }

        static bool ShouldBeEmbedded(PropertyInfo property)
        {
            return property.GetCustomAttributes(typeof (EmbeddedResourceAttribute), true).Count() > 0;
        }

        Data CreateTemplate(PropertyInfo property)
        {
            var data_item_annotation = property.GetCustomAttributes(typeof(RepresentationDataItemAttribute), true).FirstOrDefault() as RepresentationDataItemAttribute;
            
            var name = property.Name;
            var prompt = property.Name;
            if (data_item_annotation != null)
            {
                name = data_item_annotation.Name ?? property.Name;
                prompt = data_item_annotation.Prompt ?? property.Name;
            }
            

            var data_item = new Data
                                {
                                    name = name,
                                    prompt = prompt,
                                    type = property.PropertyType.Name
                                    
                                };
            return data_item;
        }

        bool should_be_converted_to_a_data_item(PropertyInfo property)
        {
            return true;
        }
    }
}