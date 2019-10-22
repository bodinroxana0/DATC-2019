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
            new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials("datcdemoluni", "0iC24GOBAlLYUmGebdyEcmrMdxAMvwtKkLmfNy4mjF7dpigvoXGMU2VSWxEpDUXi5H3czl3+Z2TAYaqpY0nAhw=="), true);
             // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();      
            // Get a reference to a table named "peopleTable"
            StudentiBR = tableClient.GetTableReference("StundetiBR");
            Task.Run(async()=>await Init()).GetAwaiter().GetResult();
           
           
        }
        static async Task Init()
        {
           // await CreatePeopleTableAsync();
            //await InsertTableAsync();
            //await SelectAsync();
            //await Delete();
            await SelectAsync();
            await UpdateTableAsync();
            await SelectAsync();

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
            StudentEntity student1 = new StudentEntity("UMFT-M", "LO122");
            student1.FirstName = "Ale";
            student1.LastName = "Gricz";
            student1.Year= 3;

            // Add both customer entities to the batch insert operation.
            batchOperation.Insert(student1);
            // Execute the batch operation.
            await StudentiBR.ExecuteBatchAsync(batchOperation);
        }
        static async Task SelectAsync()
        {
        // Construct the query operation for all customer entities where PartitionKey="Smith".
        TableQuery<StudentEntity> query = new TableQuery<StudentEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "UMFT-M"));

        // Print the fields for each customer.
        TableContinuationToken token = null;
        do
        {
            TableQuerySegment<StudentEntity> resultSegment = await StudentiBR.ExecuteQuerySegmentedAsync(query, token);
            token = resultSegment.ContinuationToken;

            foreach (StudentEntity entity in resultSegment.Results)
            {
                Console.WriteLine("{0}, {1}\t{2}\t{3} , {4}", entity.PartitionKey, entity.RowKey, entity.FirstName,
                entity.LastName, entity.Year);
            }
        } while (token != null);

    }
        static async Task Delete(){ 
        // Create a retrieve operation that expects a customer entity.
        TableOperation retrieveOperation = TableOperation.Retrieve<StudentEntity>("UMFT-M", "LO162");

        // Execute the operation.
        TableResult retrievedResult = await StudentiBR.ExecuteAsync(retrieveOperation);

        // Assign the result to a CustomerEntity object.
        StudentEntity deleteEntity = (StudentEntity)retrievedResult.Result;

        // Create the Delete TableOperation and then execute it.
        if (deleteEntity != null)
        {
        TableOperation deleteOperation = TableOperation.Delete(deleteEntity);

        // Execute the operation.
        await StudentiBR.ExecuteAsync(deleteOperation);

        Console.WriteLine("Entity deleted.");
        }

        else
        Console.WriteLine("Couldn't delete the entity.");
        }
         static async Task UpdateTableAsync()
        {
            // Create the batch operation.
            TableBatchOperation batchOperation = new TableBatchOperation();
            // Create a student entity and add it to the table.
            StudentEntity student1 = new StudentEntity("UMFT-M", "LO122");
            student1.FirstName = "Ale";
            student1.LastName = "Gricz";
            student1.Year= 4;

            // Add both customer entities to the batch insert operation.
            batchOperation.InsertOrReplace(student1);
            // Execute the batch operation.
            await StudentiBR.ExecuteBatchAsync(batchOperation);
        }
    }
}
