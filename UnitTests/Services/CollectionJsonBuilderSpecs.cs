using AvenidaSoftware.HypermediaTools.Builders;
using AvenidaSoftware.HypermediaTools.Services;
using FizzWare.NBuilder.Generators;
using Machine.Fakes;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;

namespace HypermediaTools.UnitTests.Services {
	
	[Subject(typeof(CollectionJsonBuilder))]  
	public class CollectionJsonBuilderSpecs{
		public abstract class concern : Observes<ICollectionJsonBuilder, CollectionJsonBuilder>{
			Establish c = () => {
				collection_json_configuration =  depends.on<ICollectionJsonConfiguration>();
			};

			protected static ICollectionJsonConfiguration collection_json_configuration;
		}

		public class when_getting_the_collection_json_builder : concern{
			Establish c = () => { url = GetRandom.String(21); };

			Because b = () => result = sut.Create(url);

			It should_set_the_url_to_the_collection = () => collection_json_configuration.WasToldTo(x => x.SetUrl(url));
				   
			It should_return_a_create_collection_json_service = () => result.ShouldBeTheSameAs(collection_json_configuration);

			static string url;

			static ICollectionJsonConfiguration result;
		}
	}

}
