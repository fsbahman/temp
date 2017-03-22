using Newtonsoft.Json;
using StorageCleaner.Support.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageCleaner.Support
{
    public class ArgumentRepository
    {
        private Command[] _commands;
        public ArgumentRepository()
        {
        }

        public ArgumentRepository(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
                throw new ArgumentException("File does not exists", nameof(fileName));
            }
            string jsonContent = System.IO.File.ReadAllText(fileName);
            _commands = Init(jsonContent);
        }

        private Command[] Init(string jsonContent)
        {
            var json = JsonConvert.DeserializeObject<Rootobject>(jsonContent);
            return json.args.commands;
        }

        public class Rootobject
        {
            public Args args { get; set; }
        }

        public Argument Parse(string[] args)
        {
            if(args.Length == 0)
            {
                throw new ArgumentException("No Param specified!");
            }
            return null;
        }
    }
}
