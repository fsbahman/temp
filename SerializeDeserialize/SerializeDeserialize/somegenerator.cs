using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializeDeserialize
{
	public class somegenerator
	{
		public string Get()
		{
			somedata data = new somedata()
			{
				id = 1,
				name = "bahman",
				now = DateTime.Now
			};
			return Newtonsoft.Json.JsonConvert.SerializeObject(data);
		}
	}
}
