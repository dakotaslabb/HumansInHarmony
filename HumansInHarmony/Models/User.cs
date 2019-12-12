using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumansInHarmony.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<LikedSongs> Likes { get; set; } = new List<LikedSongs>();
        public List<DislikedSongs> Dislikes { get; set; } = new List<DislikedSongs>();
    }
}
