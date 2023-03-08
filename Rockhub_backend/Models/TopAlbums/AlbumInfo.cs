using Newtonsoft.Json;

namespace RockHub_Backend.Models.Artists.TopAlbums
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Album
    {
        public string artist { get; set; }
        public string mbid { get; set; }
        public Tags tags { get; set; }
        public string playcount { get; set; }
        public List<Image> image { get; set; }
        public Tracks tracks { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string listeners { get; set; }
        public Wiki wiki { get; set; }
    }

    public class Artist
    {
        public string url { get; set; }
        public string name { get; set; }
        public string mbid { get; set; }
    }

    public class Attr
    {
        public int rank { get; set; }
    }

    public class Image
    {
        public string size { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class RootAlbumInfo
    {
        public Album album { get; set; }
    }

    public class Streamable
    {
        public string fulltrack { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Tag
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class Tags
    {
        public List<Tag> tag { get; set; }
    }

    public class Track
    {
        public Streamable streamable { get; set; }
        public int? duration { get; set; }
        public string url { get; set; }
        public string name { get; set; }

        [JsonProperty("@attr")]
        public Attr Attr { get; set; }
        public Artist artist { get; set; }
    }

    public class Tracks
    {
        public List<Track> track { get; set; }
    }

    public class Wiki
    {
        public string published { get; set; }
        public string summary { get; set; }
        public string content { get; set; }
    }

}
