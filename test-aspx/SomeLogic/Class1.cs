using NeededLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeLogic
{
    public class Class1
    {
        public int sum(int a, int b)
        {
            return a + b;
        }

        public int SumPlus2(int a,int b)
        {
            var class2 = new Class2();
            return class2.Plust2(a + b);
        }
    }
}
