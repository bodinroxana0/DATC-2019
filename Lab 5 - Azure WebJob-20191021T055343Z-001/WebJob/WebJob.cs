using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace WebJob
{
    class WebJob
    {
        private static CloudTable studentsTable;
        
        private static CloudTable raportTable;

        static void Main(string[] args)
        {
                Task.Run(async() =>
                {
                    await Init();
                })
                .GetAwaiter()
                .GetResult();
        }
    
       
        static async Task Init()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;"
                            + "AccountName=datcdemovineri"
                            + ";AccountKey=yDSrMBCO7cHeRHJMkTbG1MZ3i7HKKWOMdcoHhe4N3iFn/VCZYgnN1AQ5Ga/LxidOzgv0llbT4ZXpUU2UO+12wQ=="
                            + ";EndpointSuffix=core.windows.net";

            var account = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = account.CreateCloudTableClient();

            studentsTable = tableClient.GetTableReference("StudentiBR");
            await studentsTable.CreateIfNotExistsAsync();
            //creare tabel raport 
            raportTable=tableClient.GetTableReference("RaportBR");
            await AddReport();
        }

        static async Task<List<StudentEntity>> GetStudents()
        {
            if (studentsTable == null)
            {
                throw new Exception();
            }

            var students = new List<StudentEntity>();
            TableQuery<StudentEntity> query = new TableQuery<StudentEntity>(); //.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Smith"));

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<StudentEntity> resultSegment = await studentsTable.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                students.AddRange(resultSegment.Results);
            } while (token != null);

            return students;
        }
        static async Task<TableResult> AddReport()//numar studenti
        {
            await raportTable.CreateIfNotExistsAsync();
            var students=await GetStudents();
            if (raportTable == null)
            {
                throw new Exception();
            }
            var raport=new Raport(students.Count);
            var insertOperation = TableOperation.Insert(raport);
            return await raportTable.ExecuteAsync(insertOperation);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}