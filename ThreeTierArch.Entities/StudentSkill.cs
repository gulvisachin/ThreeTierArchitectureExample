using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeTierArch.Entities
{
    //Student(1)---------(*)StudnetSkill(*)--------(1)Skill
    public class StudentSkill
    {
        public int StudentId { get; set; } //Foreign Key

        public Student Student { get; set; }
        public int SkillId { get; set; } //Foreign Key

        public Skill Skill { get; set; } 
    }
}
