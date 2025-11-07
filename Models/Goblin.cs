using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W9_assignment_template.Models
{
    public class Goblin : Character
    {
        // Common properties for all monsters
        public int AggressionLevel { get; set; }
        
        public virtual ICollection<Ability> Abilities { get; set; }
    }
}
