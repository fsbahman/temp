using selfhost.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dispatcher;

namespace selfhost
{
    public class WebAPILoader : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            ICollection<Assembly> defaultAssemblies = base.GetAssemblies();
            List<Assembly> assemblies = new List<Assembly>(defaultAssemblies);
            Type t = typeof(CustomerController);
            Assembly a = t.Assembly;
            defaultAssemblies.Add(a);
            return assemblies;
        }
    }
}
