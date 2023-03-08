using Newtonsoft.Json;

namespace RockHub_Backend.Models.Artists
{
    public class Artist
    {
        public string? name;
        public string? mbid;
        public string? url;
        public List<Image>? image;
        public string? streamable;
        public string? ontour;
        public Stats? stats;
        public Similar? similar;
        public Tags? tags;
        public Bio? bio;
    }

    public class Bio
    {
        public Links? links;
        public string? published;
        public string? summary;
        public string? content;
    }

    public class Image
    {
        [JsonProperty("#text")]
        public string? Text;
        public string? size;
    }

    public class Link
    {
        //[JsonProperty("#text")]
        public string? Text;
        public string? rel;
        public string? href;
    }

    public class Links
    {
        public Link? link;
    }

    public class Similar
    {
        public List<Artist>? artist;
    }

    public class Stats
    {
        public string? listeners;
        public string? playcount;
    }

    public class Tag
    {
        public string? name;
        public string? url;
    }

    public class Tags
    {
        public List<Tag>? tag;
    }
    public class RootArtist
    {
        public Artist? artist;
    }
}
