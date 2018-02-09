using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthXamarin.Model
{
    public class CommentRequest
    {
        public int IdComment { get; set; }
        public string Comment1 { get; set; }
        public int IdComplain { get; set; }
        public int IdUser { get; set; }
    }
}
