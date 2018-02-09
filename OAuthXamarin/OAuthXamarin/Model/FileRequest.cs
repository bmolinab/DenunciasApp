using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthXamarin.Model
{
    public class FileRequest
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] File { get; set; }
    }

    public class DataFile
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public byte[] FileData { get; set; }
    }
}
