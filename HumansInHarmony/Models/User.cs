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
        public List<SongInfo> Likes { get; set; } = new List<SongInfo>();
        public List<SongInfo> Dislikes { get; set; } = new List<SongInfo>();
    }
}
