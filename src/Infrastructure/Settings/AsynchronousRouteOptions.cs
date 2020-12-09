using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hermes.Gateway.Ocelot.Infrastructure.Settings
{
    public class AsynchronousRouteOptions
    {
        public bool? NeedsAuthentication { get; set; }
        public string Queue { get; set; }
        public string Topic { get; set; }
    }
}
