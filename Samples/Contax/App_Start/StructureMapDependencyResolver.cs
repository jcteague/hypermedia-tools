using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using StructureMap;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace Contax {
    public class StructureMapDependencyResolver : IDependencyResolver {
        readonly IContainer container;

        public StructureMapDependencyResolver( IContainer container ) {
            this.container = container;
        }

        public object GetService( Type service_type ) {
            if( service_type.IsAbstract || service_type.IsInterface )
                return container.TryGetInstance( service_type );

            return container.GetInstance( service_type );
        }

        public IEnumerable<object> GetServices( Type service_type ) {
            return container.GetAllInstances<object>( ).Where( s => s.GetType( ) == service_type );
        }

        public IDependencyScope BeginScope( ) {
            return this;
        }

        public void Dispose( ) {}
    }
}