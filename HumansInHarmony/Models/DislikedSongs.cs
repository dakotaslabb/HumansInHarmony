using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HumansInHarmony.Models
{
    public class DislikedSongs
    {
        [Key]
        public int Id { get; set; }
        public int TrackId { get; set; }
        public string ArtistName { get; set; }
        public string CollectionName { get; set; }
        public string TrackName { get; set; }
        public string PreviewUrl { get; set; }
        public string ArtworkUrl100 { get; set; }

        public DislikedSongs()
        {

        }

        public DislikedSongs(JToken token)
        {
            JToken[] Results = token["results"].ToArray();
            foreach (var item in Results)
            {
                this.ArtistName = item["artistName"].ToString();
                this.CollectionName = item["collectionName"].ToString();
                this.TrackName = item["trackName"].ToString();
                this.PreviewUrl = item["previewUrl"].ToString();
                this.ArtworkUrl100 = item["artworkUrl100"].ToString();
                this.TrackId = int.Parse(item["trackId"].ToString());
            }
        }
    }
}
