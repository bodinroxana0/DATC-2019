using System;
using System.Threading.Tasks;

namespace WebJob
{
    class Program
    {
        static void Main(string[] args)
        {
           await Get();
          
        }
        async Task Get()
        {
            using (var service = new StudentsService())
            {
                await service.Initialize();
                var studenti= await service.GetStudents();
            }
        }
    }
}
