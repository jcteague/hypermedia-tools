

using Contax.Services;
using HypermediaTools.CollectionBuilders;
using HypermediaTools.Serialization;
using StructureMap;

namespace Contax {
    public class StructureMapBoostrapper {
        public static void Start( ) {
            ObjectFactory.Initialize( x => {

                x.Scan(scanner =>
                    {

                        scanner.AssemblyContainingType(typeof(IBuildCollection<>));
                        scanner.AssemblyContainingType<StructureMapBoostrapper>();
                        scanner.RegisterConcreteTypesAgainstTheFirstInterface();
                        scanner.ConnectImplementationsToTypesClosing(typeof(IBuildCollection<>));
                        scanner.ConnectImplementationsToTypesClosing(typeof(IBuildCollectionItems<>));
                        scanner.ConnectImplementationsToTypesClosing(typeof(IFormatAsDataItem<>));
                        scanner.ConnectImplementationsToTypesClosing(typeof(IBuildLinks<>));
                        scanner.ConnectImplementationsToTypesClosing(typeof(ITemplateBuilder<>));

                    });
                x.For<IContactRepository>().Use<ContactRepository>();
                x.For(typeof(IBuildCollection<>)).Use(typeof(CollectionBuilder<>));
                x.For(typeof(IBuildCollectionItems<>)).Use(typeof(CollectionItemBuilder<>));
                x.For(typeof(IFormatAsDataItem<>)).Use(typeof(DefaultDataItemFormatter<>));
                x.For(typeof(IBuildLinks<>)).Use(typeof(LinkBuilder<>));
                x.For(typeof(ITemplateBuilder<>)).Use(typeof(TemplateBuilder<>));
            });       
        }             
    }                 
}                     