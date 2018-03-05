using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthXamarin.Model
{
    public class ProfileData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SocialName { get; set; }
        public string Email { get; set; }
        public string ProfilePictureURL { get; set; }
        public int Denuncias { get; set; }
        public int Likes { get; set; }       
    }
}
