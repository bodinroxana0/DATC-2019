using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;

namespace laboratorul4
{
    class Program
    {
        private static CloudTableClient tableClient;
        private static CloudTable studentsTable;
        static void Main(string[] args)
        {
            Task.Run(async () => { await Initialize(); })
                .GetAwaiter()
                .GetResult();
        }

        static async Task Initialize()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;"
                + "AccountName=datcdemoluni"
                + ";AccountKey=0iC24GOBAlLYUmGebdyEcmrMdxAMvwtKkLmfNy4mjF7dpigvoXGMU2VSWxEpDUXi5H3czl3+Z2TAYaqpY0nAhw=="
                + ";EndpointSuffix=core.windows.net";

            var account = CloudStorageAccount.Parse(storageConnectionString);
            tableClient = account.CreateCloudTableClient();

            studentsTable = tableClient.GetTableReference("studenti");

            await studentsTable.CreateIfNotExistsAsync();

            //await AddNewStudent();
            //await EditStudent();
            await GetAllStudents();

        }

        private static async Task GetAllStudents()
        {
            Console.WriteLine("Universitate\tCNP\tNume\tEmail\tNumar telefon\tAn");
            TableQuery<StudentEntity> query = new TableQuery<StudentEntity>(); //.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Smith"));

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<StudentEntity> resultSegment = await studentsTable.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;

                foreach (StudentEntity entity in resultSegment.Results)
                {
                    Console.WriteLine("{0}\t{1}\t{2} {3}\t{4}\t{5}\t{6}", entity.PartitionKey, entity.RowKey, entity.FirstName, entity.LastName,
                    entity.Email, entity.PhoneNumber, entity.Year);
                }
            } while (token != null);
        }

        private static async Task AddNewStudent()
        {
            var student = new StudentEntity("UvT", "2971022253122");
            student.FirstName = "Roxana";
            student.LastName = "Bodin";
            student.Email = "roxana.bodin@gmail.com";
            student.Year = 1;
            student.PhoneNumber = "0711392830942";
            student.Faculty = "AC";

            var insertOperation = TableOperation.Insert(student);

            await studentsTable.ExecuteAsync(insertOperation);
        }

        private static async Task EditStudent()
        {
            var student = new StudentEntity("UVT", "1951014203123");
            student.FirstName = "Istvan";
            student.Year = 4;
            student.ETag = "*";
            var editOperation = TableOperation.Merge(student);

            await studentsTable.ExecuteAsync(editOperation);
        }
    }
}
