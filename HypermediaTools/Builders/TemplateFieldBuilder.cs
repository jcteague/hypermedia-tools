using System.Linq;
using AvenidaSoftware.Extensions;

namespace AvenidaSoftware.HypermediaTools.Builders {
	
	public class TemplateFieldBuilder< T > {
		readonly Template template;
		readonly Data current_data;

		public TemplateFieldBuilder( Template template, string name ) {
			this.template = template;
			current_data = new Data { name = name, prompt = name.wordify(), type = "string" };
		}

		public TemplateFieldBuilder<T> with_acceptable_values( dynamic new_value ) {
			current_data.acceptable_values = new_value;
			return this;
		}

		public TemplateFieldBuilder<T> with_value( object new_value ) {
			current_data.value = new_value.to_string();
			return this;
		}

		public TemplateFieldBuilder<T> with_prompt( object new_prompt ) {
			current_data.prompt = new_prompt.to_string();
			return this;
		}

		public TemplateFieldBuilder<T> with_type( string new_type ) {
			current_data.type = new_type;
			return this;
		}

		public ManualTemplateBuilder<T> and( ) {
			add_current_data_to_template( );
			return new ManualTemplateBuilder<T>( template );
		}

		public Template build( ) {
			add_current_data_to_template( );
			return template;
		}

		void add_current_data_to_template( ) {
			var datas = template.data.ToList( );
			datas.Add( current_data );
			template.data = datas;
		}
	}

}