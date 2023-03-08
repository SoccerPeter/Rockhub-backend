using System.Collections.Generic;
using Newtonsoft.Json;
namespace RockHub_Backend.Models.Artists.TopAlbums
{
    public class TopAlbums
    {
        public class Album
        {
            public string name;
            public int playcount;
            public string mbid;
            public string url;
            public Artist artist;
            public List<Image> image;
        }

        public class Artist
        {
            public string name;
            public string mbid;
            public string url;
        }

        public class Attr
        {
            public string artist;
            public string page;
            public string perPage;
            public string totalPages;
            public string total;
        }

        public class Image
        {
            [JsonProperty("#text")]
            public string Text;
            public string size;
        }

        public class RootTopAlbums
        {
            public Topalbums topalbums;
        }

        public class Topalbums
        {
            public List<Album> album;

            [JsonProperty("@attr")]
            public Attr Attr;
        }

    }
}
