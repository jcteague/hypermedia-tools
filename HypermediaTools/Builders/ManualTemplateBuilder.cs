using System;
using System.Linq.Expressions;

namespace AvenidaSoftware.HypermediaTools.Builders {

	public class ManualTemplateBuilder< T > {
		readonly Template template;

		public ManualTemplateBuilder( Template template ) {
			this.template = template;
		}

		public TemplateFieldBuilder<T> field( string name ) {
			return new TemplateFieldBuilder<T>( template, name );
		}

		public TemplateFieldBuilder<T> field< TResult >( Expression<Func<T, TResult>> exp ) {
			return new TemplateFieldBuilder<T>( template, get_property_name( exp ) );
		}

		static string get_property_name<TObject, TResult>(Expression<Func<TObject, TResult>> exp){
			return (((MemberExpression)(exp.Body)).Member).Name;
		}
	}

}