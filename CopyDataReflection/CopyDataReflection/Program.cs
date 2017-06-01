using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyDataReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            DataHolder x = new DataHolder() { A = 10, B = 20 };
            DataHolder y = new DataHolder();
            Copy(x, y);
            System.Console.WriteLine(y.A);
        }

        private static void Copy(DataHolder x, DataHolder y)
        {
            var classType = x.GetType();
            var flds = classType.GetFields();
            var ytype = y.GetType();

            //foreach (var field in flds)
            //{
            //    ytype.GetField(field.Name).SetValue(y, field.GetValue(x));
            //}

            var yflds = y.GetType().GetFields();
            //foreach (var field in yflds)
            //{
            //    field.SetValue(y, flds.GetValue(field.Name));
            //}

            for (int i = 0; i < yflds.Length; i++)
            {
                yflds[i].SetValue(y, flds[i].GetValue(x));
            }

            System.Console.WriteLine(y.B);







            //foreach (var fld in xflds)
            //{
            //    fld.GetValue(x)
            //}

        }
    }

    class DataHolder
    {
        public int A;
        public int B;
    }
}
