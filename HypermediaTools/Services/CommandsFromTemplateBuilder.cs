using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AvenidaSoftware.Extensions;
using AvenidaSoftware.Objects;

namespace AvenidaSoftware.HypermediaTools.Services {
	
	public class CommandsFromTemplateBuilder : IBuildCommandsFromTemplates {
        public CommandType BuildFrom< CommandType >( TemplateModel template_model ) where CommandType : class, ICommand {
            var command = typeof( CommandType ).CreateUsingDefaultConstructor( );
            SetPropertiesTo( command, template_model );
            return command.As<CommandType>( );
        }

        static void SetPropertiesTo( object obj, TemplateModel template_model ) {
            var properties = obj.get_public_writable_instance_properties( );

            foreach( var property_info in properties ) {
                var property_name = property_info.Name;
                var property_type = property_info.PropertyType;

                if( property_type.Inherits<Enumeration>( ) ) {
                    var get_enumeration_method = typeof( TemplateModelExtentions ).GetPublicStaticMethods( "GetEnumeration" ).First( );
                    var enumeration = get_enumeration_method.Invoke( null, new object [ ] { template_model, property_type, property_name } );
                    property_info.SetValue( obj, enumeration, null );
                    continue;
                }

                if( property_type.Inherits<IList<>>( ) ) {
                    var datas = template_model.template.data.Where( x => x.name.Contains(property_name) || x.name.pluralize().Contains(property_name) );
                    var conversion_type = property_type.GetGenericArguments( ).First( );
                    var template_data = datas.Where( x => !string.IsNullOrEmpty( x.value ) );
                    IEnumerable<object> values = null;

                    // TODO: refactor with specification pattern
                    if( conversion_type == typeof(CustomFieldView) ){
                       values = template_data.Select(x => new CustomFieldView { Id =  Convert.ToInt64(x.name.Split('[')[1].Replace("]", "")),Value = x.value });

                    }else {
                         values = template_data.Select( x => Convert.ChangeType( x.value, conversion_type ) );
                    }

                    var list = ( IList ) Activator.CreateInstance( typeof( List<> ).MakeGenericType( conversion_type ) );

                    values.ForEach( x => list.Add( x ) );

                    property_info.SetValue( obj, list, null );

                    continue;
                }

                if( IsNotAComponent( property_type ) ) {
                    var get_template_value_method = typeof( TemplateModelExtentions ).GetPublicStaticMethods( "GetValue" ).WithGenericParameters( 1 );
                    var get_template_value_method_of_property_type = get_template_value_method.MakeGenericMethod( property_type );
                    var template_value = get_template_value_method_of_property_type.Invoke( null, new object [ ] { template_model, property_name } );
                    property_info.SetValue( obj, template_value, null );
                    continue;
                }

                var component_data = MapComponentDataFromTemplateModel( template_model, property_name );
                var component = Activator.CreateInstance( property_type );

                SetPropertiesTo( component, component_data );
                property_info.SetValue( obj, component, null );
            }
        }

        static TemplateModel MapComponentDataFromTemplateModel( TemplateModel template_model, string property_name ) {
            // Takes the inner text of a component specified inside the brakets: 
            // Example: LegalAddress[Line1] -> Matches -> Line1
            var regular_expression_match = string.Format( "^{0}\\[(.*)\\]$", property_name );

            var datas = template_model.template.data.Select( x => new { Data = x, MatchedText = Regex.Match( x.name, regular_expression_match ) } ).Where( x => x.MatchedText.Success ).Select( x => new Data { name = x.MatchedText.Groups[ 1 ].Value, value = x.Data.value } );

            return new TemplateModel { template = new Template { data = datas } };
        }

        static bool IsNotAComponent( Type property_type ) {
            return property_type.Namespace == "System";
        }
    }

	// TODO: determine whether this class is part of the project or not
    public class CustomFieldView{
        public string Value { get; set; }
        public long Id { get; set; }
    }
}