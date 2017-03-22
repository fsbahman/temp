using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
	class Program
	{
		static void Main(string[] args)
		{
			string x = (new SerializeDeserialize.somegenerator()).Get();
			object obj = Newtonsoft.Json.JsonConvert.DeserializeObject(x);
		}
	}
}
