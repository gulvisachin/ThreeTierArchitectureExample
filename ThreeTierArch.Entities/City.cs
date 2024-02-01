using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeTierArch.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Default City";
        public int StateId { get; set; } //Foreign Key

        //Navigation Property
        public State? State { get; set; }
    }
}
