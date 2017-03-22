using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FirstDependency
{
    public class Class2
    {
        public void PlayWithAppDomains()
        {
            var newDomain = AppDomain.CreateDomain("newDomain");
            var x = newDomain.CreateInstanceAndUnwrap("", "");
            var y = Assembly.Load("");
            //Activator.CreateInstance()
            //y.
            //newDomain.CreateInstanceAndUnwrap
        }
    }
}
