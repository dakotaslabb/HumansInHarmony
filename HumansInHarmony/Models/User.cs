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
        public List<SongInfo> Likes { get; set; }
        public List<SongInfo> Dislikes { get; set; }

        public User()
        {

        }

        public User(string name, string email, string password)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }

    }
}
