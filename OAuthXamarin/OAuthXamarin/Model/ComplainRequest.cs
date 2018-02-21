using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthXamarin.Model
{
    public class ComplainRequest
    {
        public int IdComplain { get; set; }
        public int IdUser { get; set; }
        public int IdSubcategory { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string UserName { get; set; }
        public string UserPhoto { get; set; }
        public int LikesComplain { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public List<CommentRequest> listComement { get; set; }
    }
}
