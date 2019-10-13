using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace laboratorul1
{
    public static class Student_repo
    {
        public static List<Student> student_list=new List<Student>()
        { 
        new Student() { Id=1, Nume="Bodin Roxana", An=4, Facultate="AC"}
        };
    }
}