namespace AvenidaSoftware.HypermediaTools.Builders {
	
	public class BuildTemplate {
		public static ManualTemplateBuilder<object> with( Template template = null ) {
			return new ManualTemplateBuilder<object>( template ?? new Template( ) );
		}

		public static ManualTemplateBuilder<T> for_type< T >( ) {
			return new ManualTemplateBuilder<T>( new Template( ) );
		}
	}

}