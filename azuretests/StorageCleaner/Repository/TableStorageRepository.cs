using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Linq;
using System.Collections.Generic;
using StorageCleaner.Support;

namespace StorageCleaner.Repository
{
    public class TableStorageRepository
    {

        private string _connectionString;
        //private CloudTable _table;
        private CloudStorageAccount _storageAccount;
        private CloudTableClient _tableClient;
        private const int pageSize = 100;

        public TableStorageRepository(string connectionString)
        {
            _connectionString = connectionString;
            Init(connectionString);
        }

        private void Init(string connectionString)
        {
            try
            {
                _storageAccount = CloudStorageAccount.Parse(connectionString);
                _tableClient = _storageAccount.CreateCloudTableClient();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void DeleteTable(string tableName)
        {
            var table = _tableClient.GetTableReference(tableName);
            table.DeleteIfExists();
            table.Exists();
        }

        public IEnumerable<string> GetAllNames(string prefix)
        {
            var x = _tableClient.ListTables(prefix);
            return x.Select(t => t.Name);
        }

        public IEnumerable<string> GetAllNames(string[] prefixes, List<Guid> tenants)
        {
            var list = new List<string>();
            foreach (var prefix in prefixes)
            {
                foreach (var id in tenants)
                {
                    var x = _tableClient.ListTables($"{prefix}{id.ToString("N")}");
                    list.AddRange(x.Select(t => t.Name));
                }
            }

            return list;
        }

        public int DeleteAllRecords(IEnumerable<string> tables)
        {
            int res = 0;
            if (Utils.AskConsoleIfSure())
            {
                res = Utils.ExecuteAndMeasure(DeleteRecords, tables);
            }
            return res;
        }

        internal int Count(string tableName)
        {
            var table = _tableClient.GetTableReference(tableName);
            var data = GetAllRowKeys(table);
            return data.Count;
        }

        private int DeleteRecords(IEnumerable<string> tables)
        {
            int sum = 0;
            foreach (var table in tables)
            {
                Utils.ExecuteAndMeasure(() =>
                {
                    Utils.WriteColored($"Clearing: {table} ... ", ConsoleColor.White);
                    var records = DeleteAllRecordsBatch(table);
                    sum += records;
                    Utils.WriteLineColored($"Done, {records} records", ConsoleColor.Green);
                });
            }
            return sum;
        }

        private int DeleteAllRecordsBatch(string tableName)
        {
            int sum = 0;
            var table = _tableClient.GetTableReference(tableName);

            var list = GetAllRowKeys(table);

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
                    if (batchOperation.Count > 0)
                    {
                        table.ExecuteBatch(batchOperation);
                        //Utils.WriteColored($"{batchOperation.Count} deleted!\r\n", ConsoleColor.Blue);
                        sum += batchOperation.Count;
                    }
                }
                //Enumerable.Take(list.Where(t => item.PartitionKey == t.PartitionKey), 100).ToList().ForEach(t => batchOperation.Delete(t));
                //list.Where(t=> item.PartitionKey == t.PartitionKey).ToList()
            }

            return sum;
        }


        public int CleanMigrationStatusTable(IEnumerable<string> allNames)
        {
            var sum = 0;
            //var tenantsID = GetUniqueTenantIds(allNames);
            return sum;
        }

        //private IEnumerable<string> GetUniqueTenantIds(IEnumerable<string> allNames)
        //{
        //    var so = allNames.Select(f => f.StartsWith("sorows"))
        //}

        private List<DynamicTableEntity> GetAllRowKeys(CloudTable _table)
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
    }
}
