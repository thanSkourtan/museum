using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace museum
{
    [Serializable]
    public class SessionListObject
    {
        public  int id { get; set; }
        public int userId { get; set; }
        public List<SessionObject> sessionList { get; set; }
        public string sessionName { get; set; }
    }
}
