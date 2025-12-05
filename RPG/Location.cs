using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Location
    {
        private string name;
        private string description;

        public string Name { get { return name; } private set { name = value; } }
        public string Description { get { return description; } private set { description = value; } }
        public Location(string name, string description) 
        {
            Name = name;
            Description = description;
        }

        
    }
}
