using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSqlServer
{
    public class FileModel
    {
        public int id;
        public byte[] data;
        public FileModel(byte[] data, int id)
        {
            this.data = data;
            this.id = id;
        }
    }
}
