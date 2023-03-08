namespace Rockhub_backend.Models.Spotify {
    // SpotifyAlbum myDeserializedClass = JsonConvert.DeserializeObject<SpotifyAlbum>(body);
    public class Album {
        public string album_group { get; set; }
        public string album_type { get; set; }
        public List<Artist> artists { get; set; }
        public List<Copyright> copyrights { get; set; }
        public ExternalIds external_ids { get; set; }
        public ExternalUrls external_urls { get; set; }
        public List<object> genres { get; set; }
        public string id { get; set; }
        public List<Image> images { get; set; }
        public bool is_playable { get; set; }
        public string label { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string release_date { get; set; }
        public string release_date_precision { get; set; }
        public int total_tracks { get; set; }
        public Tracks tracks { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class Artist {
        public ExternalUrls external_urls { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class Copyright {
        public string text { get; set; }
        public string type { get; set; }
    }

    public class ExternalIds {
        public string upc { get; set; }
    }

    public class ExternalUrls {
        public string spotify { get; set; }
    }

    public class Image {
        public int height { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }

    public partial class Item {
        public object album { get; set; }
        public List<Artist> artists { get; set; }
        public int disc_number { get; set; }
        public int duration_ms { get; set; }
        public bool @explicit { get; set; }
        public List<object> external_ids { get; set; }
        public ExternalUrls external_urls { get; set; }
        public string id { get; set; }
        public bool is_local { get; set; }
        public bool is_playable { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string preview_url { get; set; }
        public int track_number { get; set; }
        public string type { get; set; }
        //public string uri { get; set; }
    }

    public class SpotifyAlbum {
        public List<Album> albums { get; set; }
    }

    public class Tracks {
        public List<Item> items { get; set; }
        public int limit { get; set; }
        public object next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public int total { get; set; }
    }


}
