using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace laboratorul3
{
    class Program
    {
        public static CloudTable StudentiBR ;
        static void Main(string[] args)
        {
      
            CloudStorageAccount storageAccount = new CloudStorageAccount(
            new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials("datcdemoluni", "Xf0DheNxYHU8BAyOV0snfLt3Y8R9kO7TADAvhLHi31f8LQdU04q3BTGrUKQVIV6BzvwHPYEPSfd4aElDhHxYhQ=="), true);
             // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();      
            // Get a reference to a table named "peopleTable"
            StudentiBR = tableClient.GetTableReference("StundetiBR");
            Task.Run(async()=>await Init()).GetAwaiter().GetResult();
           
           
        }
        static async Task Init()
        {
            await CreatePeopleTableAsync();
            await InsertTableAsync();
        }
        static async Task CreatePeopleTableAsync()
        {
            // Create the CloudTable if it does not exist
            await StudentiBR.CreateIfNotExistsAsync();
        }
        static async Task InsertTableAsync()
        {
            // Create the batch operation.
            TableBatchOperation batchOperation = new TableBatchOperation();
            // Create a student entity and add it to the table.
            StudentEntity student1 = new StudentEntity("UPT-AC", "LO162");
            student1.FirstName = "Roxana";
            student1.LastName = "Bodin";
            student1.Year= 4;

            // Add both customer entities to the batch insert operation.
            batchOperation.Insert(student1);
            // Execute the batch operation.
            await StudentiBR.ExecuteBatchAsync(batchOperation);
        }
        // static void Select()
        // {
        // // Construct the query operation for all customer entities where PartitionKey="Smith".
        // TableQuery<StudentEntity> query = new TableQuery<StudentEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "UPT-AC"));

        // // Print the fields for each customer.
        // TableContinuationToken token = null;
        // do
        // {
        //     TableQuerySegment<CustomerEntity> resultSegment = await peopleTable.ExecuteQuerySegmentedAsync(query, token);
        //     token = resultSegment.ContinuationToken;

        //     foreach (CustomerEntity entity in resultSegment.Results)
        //     {
        //         Console.WriteLine("{0}, {1}\t{2}\t{3}", entity.PartitionKey, entity.RowKey,
        //         entity.Email, entity.PhoneNumber);
        //     }
        // } while (token != null);

    }
}
