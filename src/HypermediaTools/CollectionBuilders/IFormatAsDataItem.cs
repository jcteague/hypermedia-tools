using System;
using System.Collections;
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
        IEnumerable<dynamic> AsDataItem(object entity, bool allowEmbedded=true);
        IEnumerable<dynamic> FormatType(Type object_type, bool allowEmbedded = true);
    }

    public class DefaultDataItemFormatter<T> : IFormatAsDataItem<T>
    {
        IFieldSerializer serializer;

        public DefaultDataItemFormatter(IFieldSerializer serializer)
        {
            this.serializer = serializer;
        }

        public IEnumerable<dynamic> AsDataItem(object entity, bool allowEmbedded = true)
        {
            var entity_type = entity.GetType();
            var entity_properties = entity_type.GetProperties();
            var result = new List<dynamic>();
          
            foreach (var property in entity_properties)
            {
                if(should_format_as_data_item(property, allowEmbedded))
                {
                    var property_obj = property.GetValue(entity, null);
                    if(ShouldBeEmbedded(property, allowEmbedded))
                    {
                        
                        var embedded_data_items = embed_data_item(property, property_obj);
                        result.Add(embedded_data_items);
                    }
                    else
                    {
                        var data_item = CreateTemplate(property);
                        data_item.value = serializer.Serialize(property.GetValue(entity, null));
                        result.Add(data_item);        
                    }
                    
                }
            }
            return result;
        }

        Data embed_data_item(PropertyInfo property, object embedded_obj)
        {


            var data_result = CreateTemplate(property);
            if(embedded_obj is IEnumerable)
            {
                

                var list_result = new List<dynamic>();
                foreach (var o in (IEnumerable) embedded_obj)
                {
                    var data_item = AsDataItem(o, false);
                    list_result.Add(new{item = data_item});
                }
                data_result.value = list_result.ToArray();
                return data_result;

            }
            data_result.value = AsDataItem(embedded_obj, false).ToArray();
            return data_result;
        }

        public IEnumerable<dynamic> FormatType(Type object_type, bool allowEmbedded = true)
        {
            var entity_properties = object_type.GetProperties();
            var result = new List<dynamic>();
          
            foreach (var property in entity_properties)
            {
                if(should_format_as_data_item(property, allowEmbedded))
                {
                    
                    var data_item = CreateTemplate(property);
                    result.Add(data_item);    
                    
                    
                }
            }
            return result;
        }

        bool should_format_as_data_item(PropertyInfo property, bool allowEmbedded)
        {
            var t = property.PropertyType;
            var is_representable = t.BaseType == typeof (IAmAResource) || t.IsGenericEnumerable();
            var should_be_embedded = ShouldBeEmbedded(property, allowEmbedded);
            if (is_representable && !should_be_embedded) return false;

            return true;


        }

        static bool ShouldBeEmbedded(PropertyInfo property, bool allowEmbedded)
        {
           var marked_as_embedded = property.GetCustomAttributes(typeof (EmbeddedResourceAttribute), true).Count() > 0;
            return allowEmbedded && marked_as_embedded;
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