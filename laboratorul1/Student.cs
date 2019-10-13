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
    public class Student
    {
        public int Id;
        public int An;
        public String Nume;
        public String Facultate;
        public int getId()
        {
            return this.Id;
        }

        public void setId(int Id)
        {
            this.Id = Id;
        }

        public int getAn()
        {
            return this.An;
        }

        public void setAn(int An)
        {
            this.An = An;
        }

        public String getNume()
        {
            return this.Nume;
        }

        public void setNume(String Nume)
        {
            this.Nume = Nume;
        }

        public String getFacultate()
        {
            return this.Facultate;
        }

        public void setFacultate(String Facultate)
        {
            this.Facultate = Facultate;
        }
        public Student(int id,String nume,int an,String facultate)
        {
            this.Id=id;
            this.Nume=nume;
            this.An=an;
            this.Facultate=facultate;
        }

    }
}
