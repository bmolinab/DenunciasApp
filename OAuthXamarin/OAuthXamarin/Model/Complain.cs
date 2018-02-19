using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthXamarin.Model
{
    public class Complain
    {
        public int IdComplain { get; set; }
        public int IdUser { get; set; }
        public int IdSubcategory { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool? Approved { get; set; }
    }
}
