using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageCleaner.Support.Model
{
    public class Command
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Param[] _params { get; set; }
    }

    public class Args
    {
        public Command[] commands { get; set; }
    }

    public class Param
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsOptional { get; set; }
    }



}
