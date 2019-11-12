using Microsoft.WindowsAzure.Storage.Table;

namespace WebJob
{
    public class Raport : TableEntity
    {
        public int Nr{get;set;}
        public Raport(int nr)
        {
            this.PartitionKey="2";
            this.RowKey="20";
            this.Nr=nr;
        }
        public Raport(){}
    }
}