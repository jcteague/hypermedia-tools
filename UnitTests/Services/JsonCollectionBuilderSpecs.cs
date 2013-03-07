using AvenidaSoftware.HypermediaTools.Builders;
using AvenidaSoftware.HypermediaTools.Services;
using FizzWare.NBuilder.Generators;
using Machine.Fakes;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;

namespace HypermediaTools.UnitTests.Services {
	
	[Subject(typeof(JsonCollectionBuilder))]  
	public class JsonCollectionBuilderSpecs{
		public abstract class concern : Observes<IJsonCollectionBuilder, JsonCollectionBuilder>{
			Establish c = () => {
				json_collection_configuration =  depends.on<IJsonCollectionConfiguration>();
			};

			protected static IJsonCollectionConfiguration json_collection_configuration;
		}

		public class when_getting_the_collection_json_builder : concern{
			Establish c = () => { url = GetRandom.String(21); };

			Because b = () => result = sut.Create(url);

			It should_set_the_url_to_the_collection = () => json_collection_configuration.WasToldTo(x => x.SetUrl(url));
				   
			It should_return_a_create_collection_json_service = () => result.ShouldBeTheSameAs(json_collection_configuration);

			static string url;

			static IJsonCollectionConfiguration result;
		}
	}

}
