using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;

namespace HumansInHarmony.Models
{
    public class ItunesDAL
    {

        public static SongInfo FindSong(int arrayNumber)
        {
            
            string sondId = SongsArray.Songs[arrayNumber];
            HttpWebRequest request = WebRequest.CreateHttp($"https://itunes.apple.com/search?term={songId}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());

            string APIText = rd.ReadToEnd();

            JToken token = JToken.Parse(APIText);

            SongInfo song = new SongInfo(token);

            return song;
        }

        public static SongInfo SaveSong(string Id)
        {
            HttpWebRequest request = WebRequest.CreateHttp($"https://itunes.apple.com/search?term={Id}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());

            string APIText = rd.ReadToEnd();

            JToken token = JToken.Parse(APIText);

            SongInfo song = new SongInfo(token);

            return song;
        }
    }
}
