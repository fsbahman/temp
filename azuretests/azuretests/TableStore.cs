using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azuretests
{
    public class TableStore
    {
        private CloudTable _table;

        public TableStore()
        {
            //var connectionString = "DefaultEndpointsProtocol=https;AccountName=exactsalesorder;AccountKey=FC9Qsr9df8FZaMkjHX9nqUaNlnrtfo0OJ6kT201P/N52HkgcUXBQ3DwMAcQXW9npTP/goXUmNUd3mCpb5nnZdg==";
            //var connectionString = "DefaultEndpointsProtocol=https;AccountName=fsbahman;AccountKey=wiVmrKH6KdML24nMso35n+ASil1XtMhpRPFVRmvoZdFcHg13zkCipkjW8M9n+OVFAbTDd/HND7jxTczIFzFmtw==";
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=eolsalesordereuwpstoacc1;AccountKey=KQCUSxh0HDblTxf1LsOQtP6nRR6xRovXX8+EKn/+H33W7n2GqpqYn45I0+N94iQ3JsU1SIcMZbwVeBSi+AW8/A==";
            var tableName = "tenantregistry";
            Init(connectionString, tableName);
        }

        public void ModifyTopRows(int top, int skip)
        {
            var tenants = LoadTenants();
            var topOnes = tenants.OrderBy(t => t.Properties["Name"].StringValue).Skip(skip).Take(top).ToList();
            topOnes.ForEach(t => AddAllowEventColumn(t));
            var start = System.Diagnostics.Stopwatch.StartNew();
            topOnes.ForEach(t =>
            {
                _table.Execute(TableOperation.InsertOrMerge(t));
                Console.Write($"<{t.Properties["Name"].StringValue}>"); Utility.WriteColored(" Updated", ConsoleColor.DarkMagenta);
            });
            start.Stop();
            Utility.WriteColored((start.ElapsedMilliseconds / 1000).ToString("00:00 s"), ConsoleColor.DarkYellow);
        }

        void ExecuteAndMeasure(Action action)
        {
            var start = System.Diagnostics.Stopwatch.StartNew();
            action();
            start.Stop();
            Utility.WriteColored((start.ElapsedMilliseconds / 1000).ToString("00:00 s"), ConsoleColor.DarkCyan);
        }

        public void DeleteAll()
        {
            if (Utility.AskConsoleIfSure())
            {
                ExecuteAndMeasure(DeleteRecords);
            }
        }

        private const int pageSize = 100;

        private void DeleteRecords()
        {
            var list = GetAllRowKeys();

            var x = list.GroupBy(t => t.PartitionKey).Select(g => g.First()).ToList();
            foreach (var item in x)
            {
                var partitionData = list.Where(t => item.PartitionKey == t.PartitionKey);
                int page = 0;
                int count = partitionData.Count();
                while (page <= count / pageSize)
                {
                    var batchOperation = new TableBatchOperation();
                    partitionData
                        .Skip(pageSize * page++)
                        .Take(pageSize).ToList().ForEach(t => batchOperation.Delete(t));
                    _table.ExecuteBatch(batchOperation);
                    Utility.WriteColored($"{batchOperation.Count} deleted!\r\n", ConsoleColor.Blue);
                }
                //Enumerable.Take(list.Where(t => item.PartitionKey == t.PartitionKey), 100).ToList().ForEach(t => batchOperation.Delete(t));
                //list.Where(t=> item.PartitionKey == t.PartitionKey).ToList()
            }
        }

        //private 

        private List<DynamicTableEntity> GetAllRowKeys()
        {
            var token = default(TableContinuationToken);
            var data = new List<DynamicTableEntity>();
            var query = new TableQuery().Select(new string[] { "PartitionKey", "RowKey" });
            do
            {
                var queryResult = _table.ExecuteQuerySegmented(new TableQuery(), token);
                token = queryResult.ContinuationToken;
                data.AddRange(queryResult.Results);
            }
            while (token != null);
            return data;
        }

        private List<DynamicTableEntity> LoadTenants()
        {
            var token = default(TableContinuationToken);
            var data = new List<DynamicTableEntity>();
            do
            {
                var queryResult = _table.ExecuteQuerySegmented(new TableQuery(), token);
                token = queryResult.ContinuationToken;
                data.AddRange(queryResult.Results);
            }
            while (token != null);
            return data;
        }

        private void AddAllowEventColumn(DynamicTableEntity te)
        {
            if (te.Properties.ContainsKey("AllowEvent"))
            {
                te.Properties["AllowEvent"] = new EntityProperty(true);
            }
            else
            {
                te.Properties.Add("AllowEvent", new EntityProperty(true));
            }

        }

        private void Init(string connectionString, string tableName)
        {
            try
            {
                var storageAccount = CloudStorageAccount.Parse(connectionString);
                this._table = storageAccount.CreateCloudTableClient().GetTableReference(tableName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}
