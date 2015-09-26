using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace museum
{
    
    [Serializable]
    public class SessionObject
    {
        public string Name {get; set;}

        public Type type { get; set; }
        
        //public EventArgs e { get; set; }
        public MouseButtons button {get; set;}

    }
}
