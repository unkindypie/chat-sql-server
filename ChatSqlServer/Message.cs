using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSqlServer
{
    public class Message
    {
        public int id;
        public string text;
        public string time;
        public User user;
        public FileModel file; 
        public Message()
        {

        }
        public Message(string text, User user, FileModel file)
        {
            this.text = text;
            this.user = user;
            this.file = file;
        }
        public override string ToString()
        {
            return user.ToString() + "\t" + time + "\n " + text;
        }
    }
}
