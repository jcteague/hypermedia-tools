using StructureMap;
using StructureMap.Configuration.DSL;

namespace AvenidaSoftware.HypermediaTools {
	public class Configuration {
		 public static void Start() {
		 	ObjectFactory.Initialize( x => x.AddRegistry<StructureMapRegistry>() );
		 }
	}

	public class StructureMapRegistry : Registry {
		public StructureMapRegistry() {
			Scan( scanner => {
                scanner.AssemblyContainingType<StructureMapRegistry>();
                scanner.RegisterConcreteTypesAgainstTheFirstInterface();
			});
		}
	}
}