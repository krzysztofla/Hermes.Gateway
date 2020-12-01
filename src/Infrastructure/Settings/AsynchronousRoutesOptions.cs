using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hermes.Gateway.Ocelot.Infrastructure.Settings
{
    public class AsynchronousRoutesOptions
    {
        public bool? IsAuthenticated { get; set; }
        public IDictionary<string, AsynchronousRouteOptions> Routes {get; set;}
    }
}
