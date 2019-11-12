using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Models;
using Services;

namespace StudentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        const string ServiceBusConnectionString = "DefaultEndpointsProtocol=https;AccountName=proiectdatc;AccountKey=z8vcMZ8jKANXekLr7aRh0JQ9w9eYkHn++3N1Yzbb3MXhMclcizLVS05QaPAXo4U0ZAZDk1z7Mmf3UY2eIHujQQ==;EndpointSuffix=core.windows.net";
        const string QueueName = "queue-1";
        static IQueueClient queueClient;

        // GET api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentEntity>>> Get()
        {
            using (var service = new StudentsService())
            {
                await service.Initialize();
                return await service.GetStudents();
            }
        }

        // GET api/students/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            // Must be implemented
            return null;
        }

        // POST api/students
        [HttpPost]
        public async Task Post([FromBody] StudentModel student)
        {
            if (string.IsNullOrEmpty(student.University) || string.IsNullOrEmpty(student.CNP))
            {
                throw new Exception("The student does not have university or CNP set!");
            }

            var studentEntity = new StudentEntity(student.University, student.CNP)
            {
                Email = student.Email,
                Faculty = student.Faculty,
                FirstName = student.FirstName,
                LastName = student.LastName,
                PhoneNumber = student.PhoneNumber,
                Year = student.Year
            };
            
            // queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
            // string json = JsonConvert.SerializeObject(student);
            // var message = new Message(Encoding.UTF8.GetBytes(json));
            // Console.WriteLine($"Sending message: {json}");
            //  // Send the message to the queue.
            // await queueClient.SendAsync(message);

            using (var service = new StudentsService())
            {
                await service.Initialize();
                await service.AddStudent(studentEntity);
            }
        }

        // PUT api/students/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            // Must be implemented
        }

        // DELETE api/students/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // Must be implemented
        }
    }
}
