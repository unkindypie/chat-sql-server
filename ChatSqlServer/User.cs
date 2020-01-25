using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSqlServer
{
    public class User
    {
        public int id;
        public string name;
        public override string ToString()
        {
            return name;
        }
    }
}
