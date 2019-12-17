using System;
using System.Collections.Generic;

namespace HumansInHarmony.Models
{
    public partial class User
    {
        public User()
        {
            DislikedSongs = new HashSet<DislikedSongs>();
            LikedSongs = new HashSet<LikedSongs>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Snapchat { get; set; }
        public string Twitter { get; set; }

        public virtual ICollection<DislikedSongs> DislikedSongs { get; set; }
        public virtual ICollection<LikedSongs> LikedSongs { get; set; }
    }
}
