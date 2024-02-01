using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeTierArch.Entities
{
    //Student(1)---------(*)Skill
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
