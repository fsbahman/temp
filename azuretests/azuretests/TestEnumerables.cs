using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azuretests
{
    public class TestEnumerables
    {

        public static IEnumerable<Model> GetAll2()
        {
            var list = new List<Model>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new Model()
                {
                    ID = Guid.NewGuid(),
                    Name = $"ME{i}"
                });
            }
            return list;
        }

        public static IEnumerable<Model> GetAll()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return new Model()
                {
                    ID = Guid.NewGuid(),
                    Name = $"ME{i}"
                };
            }
        }
    }

    public class Model
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Model: {this.ID}->{this.Name}";
        }
    }


}
