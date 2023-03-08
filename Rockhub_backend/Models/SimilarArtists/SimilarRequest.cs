using Newtonsoft.Json;

namespace RockHub_Backend.Models.Artists.SimilarArtists
{
    public class SimilarRequest
    {
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Artist
    {
        public string name;
        public string mbid;
        public string match;
        public string url;
        public List<Image> image;
        public string streamable;
    }

    public class Attr
    {
        public string artist;
    }

    public class Image
    {
        [JsonProperty("#text")]
        public string Text;
        public string size;
    }

    public class RootSimilar
    {
        public Similarartists similarartists;
    }

    public class Similarartists
    {
        public List<Artist> artist;

        [JsonProperty("@attr")]
        public Attr Attr;
    }
}
