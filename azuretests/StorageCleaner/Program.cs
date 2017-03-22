using StorageCleaner.Repository;
using StorageCleaner.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageCleaner
{
    class Program
    {

        static string[] prefixes = { "delivery", "invoices", "sorows", "payment" };
        static List<Guid> list = new List<Guid>()
            {
                Guid.Parse("36adf19f-486e-4826-8040-25473a1b2df0"),
                Guid.Parse("737e116f-c142-4783-a2af-bbdb3280f6fc"),
                Guid.Parse("98e2b355-850f-46a0-bddf-4761e73e905f"),
                Guid.Parse("a1fa1a5d-bd64-47b3-9158-4e44c425bec5"),
                Guid.Parse("42b7539e-67fb-462c-87a5-6cbdce81fb9d"),
                Guid.Parse("d4aee4b7-9aa4-4241-b7dd-d6debc4fcc84"),
                Guid.Parse("bd81328b-f816-4dbb-b783-437367264ac9"),
                Guid.Parse("04ec45da-9feb-4fc0-bbf3-d179e25ebf06"),
                Guid.Parse("81535bef-80c5-4d48-9615-c46719f3b43a"),
                Guid.Parse("93fca6f6-84a3-4eec-91e0-eb208888fc88"),
                Guid.Parse("a9c9778b-d7f1-4870-853b-93c02ec8ade0"),
                Guid.Parse("d3d944e2-c1a0-4114-9c8b-3877305a54ec")
            };


        static void Main(string[] args)
        {
            //var connectionString = "DefaultEndpointsProtocol=https;AccountName=exactsalesorder;AccountKey=FC9Qsr9df8FZaMkjHX9nqUaNlnrtfo0OJ6kT201P/N52HkgcUXBQ3DwMAcQXW9npTP/goXUmNUd3mCpb5nnZdg==";
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=fsbahman;AccountKey=wiVmrKH6KdML24nMso35n+ASil1XtMhpRPFVRmvoZdFcHg13zkCipkjW8M9n+OVFAbTDd/HND7jxTczIFzFmtw==";
            //var connectionString = "DefaultEndpointsProtocol=https;AccountName=eolsalesordereuwpstoacc1;AccountKey=KQCUSxh0HDblTxf1LsOQtP6nRR6xRovXX8+EKn/+H33W7n2GqpqYn45I0+N94iQ3JsU1SIcMZbwVeBSi+AW8/A==";


            var repo = new TableStorageRepository(connectionString);
            repo.DeleteTable("test1");

            //DeleteRecords(connectionString);

            //Utils.ExecuteAndMeasure(() =>
            //{
            //    GetTablesMeasuresByCRTenants(connectionString);
            //}, ConsoleColor.Magenta);


            //GetTablesMeasures(connectionString);
            //foreach (var name in all)
            //{
            //    Console.WriteLine(name);
            //}

            //handleArguments(args);
        }

        private static void DeleteRecords(string connectionString)
        {
            var repo = new TableStorageRepository(connectionString);
            var all = repo.GetAllNames(prefixes, list);
            var count = repo.DeleteAllRecords(all);
            Utils.WriteLineColored($"records:{count}", ConsoleColor.Red);
        }

        static void GetTablesMeasuresByPrefix(string connectionString)
        {
            var repo = new TableStorageRepository(connectionString);
            string[] prefixes = { "delivery", "invoice", "sorow", "payment" };


            foreach (var prefix in prefixes)
            {
                var all = repo.GetAllNames(prefix);
                all.ToList().ForEach(t =>
                {
                    var recordCount = Utils.ExecuteAndMeasure<int>(() => repo.Count(t));
                    Utils.WriteLineColored($"{t}:{recordCount}", ConsoleColor.Green);
                });
                Utils.WriteLineColored("-- Finished --", ConsoleColor.Blue);
                //Console.WriteLine(all.Count());
            }
        }

        static void GetTablesMeasuresByCRTenants(string connectionString)
        {
            var repo = new TableStorageRepository(connectionString);


            var all = repo.GetAllNames(prefixes, list);
            all.ToList().ForEach(t =>
            {
                var recordCount = Utils.ExecuteAndMeasure<int>(() => repo.Count(t));
                Utils.WriteLineColored($"{t}:{recordCount}", ConsoleColor.Green);
            });
            Utils.WriteLineColored("-- Finished --", ConsoleColor.Blue);
        }


        static void handleArguments(string[] args)
        {
            /*if (args.Length == 0) { Console.WriteLine("No Argument specified!"); return; };
            switch (args[0])
            {
                case "list":

                case "count":
                default:
                    break;
            }*/
            foreach (var str in args)
            {
                Console.WriteLine(str);
            }
        }


    }
}
