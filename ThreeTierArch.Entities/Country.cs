using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeTierArch.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //Navigation Property
        public ICollection<State> States { get; set;} = new HashSet<State>();
    }
}
