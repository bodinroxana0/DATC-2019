using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;


namespace WebJob
{
    public class StudentsService : IDisposable
    {
        private CloudTable studentsTable;

        public StudentsService()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;"
                            + "AccountName=datcdemoluni"
                            + ";AccountKey=0iC24GOBAlLYUmGebdyEcmrMdxAMvwtKkLmfNy4mjF7dpigvoXGMU2VSWxEpDUXi5H3czl3+Z2TAYaqpY0nAhw=="
                            + ";EndpointSuffix=core.windows.net";

            var account = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = account.CreateCloudTableClient();

            studentsTable = tableClient.GetTableReference("StudentiBR");
            //creare tabel raport 
        }

        public async Task Initialize()
        {
            await studentsTable.CreateIfNotExistsAsync();
            ///raport
        }

        public async Task<List<StudentEntity>> GetStudents()
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
         public async Task<TableResult> AddReport(StudentEntity student)//numar studenti
        {
            if (studentsTable == null)
            {
                throw new Exception();
            }

            var insertOperation = TableOperation.Insert(student);
            return await studentsTable.ExecuteAsync(insertOperation);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}