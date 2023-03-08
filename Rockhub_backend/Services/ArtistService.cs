using HtmlAgilityPack;
using Newtonsoft.Json;
using Rockhub_backend.Models.Spotify;
using RockHub_Backend.Models.Artists;
using RockHub_Backend.Models.Artists.SimilarArtists;
using RockHub_Backend.Models.Artists.TopAlbums;
using RockHub_Backend.Services.Interfaces;
using static RockHub_Backend.Models.Artists.TopAlbums.TopAlbums;

namespace RockHub_Backend.Services
{
    public class ArtistService : IArtist
    {
        private string allMusicKey;
        public ArtistService(IConfiguration configuration)
        {
            allMusicKey = configuration.GetValue<string>("AllMusicKey");
        }
        
        public async Task<ArtistResponse> GetArtist(string artist)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("http://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=" + artist + allMusicKey);
            RootArtist myArtist = JsonConvert.DeserializeObject<RootArtist>(response);
            var picture = GetPictureAsync(artist);
            ArtistResponse aR = new ArtistResponse
            {
                Group = myArtist.artist.name,
                GroupInfo = myArtist.artist.bio.content,
                Image = picture.Result,
                ShortInfo = myArtist.artist.bio.summary
            };
            return aR;
        }

        public async Task<List<SimilarResonse>> GetSimilar(string artist)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("http://ws.audioscrobbler.com/2.0/?method=artist.getsimilar&artist=" + artist + allMusicKey);
            RootSimilar mySimilar = JsonConvert.DeserializeObject<RootSimilar>(response);

            List<SimilarResonse> listSimilar = new List<SimilarResonse>();
            int iAntal;
            if (mySimilar.similarartists.artist.Count() > 15)
            {
                iAntal = 15;
            }
            else iAntal = mySimilar.similarartists.artist.Count();

            foreach (var item in mySimilar.similarartists.artist.Take(iAntal))
            {
                var p = await GetPictureAsync(item.name);
                var info  = await GetArtist(item.name);
                SimilarResonse sR = new SimilarResonse
                {
                    Name = item.name,
                    Pic = p,
                    InfoUrl = item.url,
                    Info = info.ShortInfo
                };
                listSimilar.Add(sR);
            }
            

            return listSimilar;
        }

        public async Task<List<TopAlbumsResponse>> GetTopAlbums(string artist)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("http://ws.audioscrobbler.com/2.0/?method=artist.gettopalbums&artist=" + artist + allMusicKey);
            RootTopAlbums myTopAlbums = JsonConvert.DeserializeObject<RootTopAlbums>(response);

            List<TopAlbumsResponse> listTop = new List<TopAlbumsResponse>();
            int iAntal;
            if (myTopAlbums.topalbums.album.Count() > 25)
            {
                iAntal = 25;
            }
            else iAntal = myTopAlbums.topalbums.album.Count();

            foreach (var item in myTopAlbums.topalbums.album.Take(iAntal))
            {
                string info = await AlbumInfoAsync(artist, item.name);
                TopAlbumsResponse tA = new TopAlbumsResponse
                {
                    Album = item.name,
                    CoverArt = item.image[3].Text,
                    AlbumInfo = info,
                };
                listTop.Add(tA);
            }


            return listTop;
        }

        private async Task<string> AlbumInfoAsync(string artist, string album)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key=b6503b47f18d83ffb949922df2b96f08&artist=" + artist + "&album=" + album + "&format=json");
            RootAlbumInfo myAlbumInfo = JsonConvert.DeserializeObject<RootAlbumInfo>(response);
            if (myAlbumInfo != null && myAlbumInfo.album.wiki != null)
            {
                return myAlbumInfo.album.wiki.content.ToString();
            }
            else return string.Empty;
        }

        private static Task<string> GetPictureAsync(string artist)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            List<Pictures> B = new List<Pictures>();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            int i = 0;
            string address = "https://www.google.com/search?q=" + artist + "&tbm=isch&gws_rd=cr&ei=16E0WMGSKYmisAHmp6b4Ag";
            doc.Load(wc.OpenRead(address));
            HtmlNodeCollection imgs = doc.DocumentNode.SelectNodes("//img[@src]");
            foreach (HtmlNode img in imgs)
            {     
                if (i == 1)
                {
                    HtmlAttribute src = img.Attributes["src"];
                    Pictures b = new Pictures();
                    b.ImageUrl = src.Value;
                    return Task.FromResult(b.ImageUrl);
                }
                i++;
            }
           return Task.FromResult(string.Empty);
        }

        public async Task<SpotifyArtist> GetSpotifyArtist(string artist) {
            var client = new HttpClient();
            var request = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://spotify23.p.rapidapi.com/search/?q=" + artist + "&type=albums&offset=0&limit=10&numberOfTopResults=5"),
                Headers =
                {
                    { "X-RapidAPI-Key", "5e8dddee63msh9035b5a843657c6p1bb53ejsn7ba86e024718" },
                    { "X-RapidAPI-Host", "spotify23.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request)) {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                SpotifyArtist mySpotifyArtist = JsonConvert.DeserializeObject<SpotifyArtist>(body);
                return mySpotifyArtist;
            }
        }

        public async Task<SpotifyAlbum> GetSpotifyAlbum(string id) {
            var client = new HttpClient();
            var request = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://spotify23.p.rapidapi.com/albums/?ids=" + id),
                Headers =
                {
                     { "X-RapidAPI-Key", "5e8dddee63msh9035b5a843657c6p1bb53ejsn7ba86e024718" },
                     { "X-RapidAPI-Host", "spotify23.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request)) {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                SpotifyAlbum myAlbum = JsonConvert.DeserializeObject<SpotifyAlbum>(body);
                return myAlbum;
            }
        }
    }
}
