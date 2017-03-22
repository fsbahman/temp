using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loadassembly
{
	class Program
	{
		static void Main(string[] args)
		{
			//someassembly.someclass
			//var x = System.Reflection.Assembly.Load("someassembly.someclass");
			//var t = Type.GetType("someassembly.someclass", true);
			//var m = Activator.CreateInstance(t, true);


			string codebase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
			Uri p = new Uri(codebase);
			string localPath = p.LocalPath.Substring(0, p.LocalPath.LastIndexOf("\\"));
			var myassembly = System.Reflection.Assembly.LoadFrom(System.IO.Path.Combine(localPath, "someassembly.dll"));
			Type tt = myassembly.GetType("someassembly.someclass");
			

		}
	}
}
