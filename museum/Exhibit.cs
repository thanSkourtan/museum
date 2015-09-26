using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace museum
{   //corresponds to the info table
    class Exhibit
    {
        public int id { get; set; }
        public string type { get; set; }
        public string place { get; set; }
        public string timing { get; set; }
        public string dimensions { get; set; }
        public string area { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public int category { get; set; }
    }
}
