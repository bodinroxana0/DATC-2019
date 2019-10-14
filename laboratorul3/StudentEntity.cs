
using Microsoft.WindowsAzure.Storage.Table;

public class StudentEntity : TableEntity
{
    public StudentEntity(string faculty, string matricol_no)
    {
        this.PartitionKey = faculty;
        this.RowKey = matricol_no;
    }

    public StudentEntity() { }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public int Year { get; set; }
}
