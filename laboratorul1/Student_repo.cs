using System;
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
    public class Student_repo
    {
        public static List<Student> student_list=new List<Student>()
        { new Student(1,"Bodin Roxana",4,"AC"),
        new Student(2,"Gricz Alexandra",4,"AC"),
        new Student(3,"Sabau Daniela",4,"AC")};
    }
}