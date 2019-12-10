using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace HumansInHarmony.Models
{
    public class ItunesDAL
    {

        public static List<SongInfo> FindSong()
        {
            List<SongInfo> songList = new List<SongInfo>();
            foreach (var songId in SongsArray.Songs)
            {

                HttpWebRequest request = WebRequest.CreateHttp($"https://itunes.apple.com/search?term={songId}");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader rd = new StreamReader(response.GetResponseStream());

                string APIText = rd.ReadToEnd();

                JToken token = JToken.Parse(APIText);

                SongInfo song = new SongInfo(token);

                songList.Add(song);
            }
            return songList;
        }

        public static LikedSongs SaveLike(string Id)
        {
            HttpWebRequest request = WebRequest.CreateHttp($"https://itunes.apple.com/search?term={Id}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());

            string APIText = rd.ReadToEnd();

            JToken token = JToken.Parse(APIText);

            LikedSongs song = new LikedSongs(token);

            return song;
        }

        public static DislikedSongs SaveDislike(string Id)
        {
            HttpWebRequest request = WebRequest.CreateHttp($"https://itunes.apple.com/search?term={Id}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());

            string APIText = rd.ReadToEnd();

            JToken token = JToken.Parse(APIText);

            DislikedSongs song = new DislikedSongs(token);

            return song;
        }
    }
}
