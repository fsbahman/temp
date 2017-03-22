using FirstDependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testAppDomaints
{
    class SomeLogic
    {
        public void DoMagic()
        {
            Class1 c1 = new Class1();
            Console.WriteLine($"For you 2 + 2 is: {c1.Sum(2, 3)} ;-)");
        }
    }
}
