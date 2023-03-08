using Microsoft.AspNetCore.Mvc;
using RockHub_Backend.Models.Artists.SimilarArtists;
using RockHub_Backend.Models.Artists.TopAlbums;
using RockHub_Backend.Models.Artists;
using RockHub_Backend.Services.Interfaces;
using System.Xml;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Rockhub_backend.Models.Spotify;
using static RockHub_Backend.Models.Artists.TopAlbums.TopAlbums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rockhub_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : BaseController
    {
        private readonly IArtist _artist;
       
        public ArtistController(IArtist artist)
        {
            _artist = artist;
        }

        /// <summary>
        /// Get info about the artist
        /// </summary>
        /// <remarks>
        /// Returns all information about the artist/group.
        /// </remarks>
        /// <param name="artist"></param>
        /// <param name="XApiKey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/artist")]
        [ProducesResponseType(typeof(ApiResult<ArtistResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetArtistAsync(string artist, [FromHeader] string XApiKey)
        {
            var A = await _artist.GetArtist(artist);
            return Ok(GetApiResult("Artist", A, success: true));
            //return A;
        }

        /// <summary>
        /// Get similar artist/group
        /// </summary>
        /// <remarks>
        /// Returns all information about similar artist/group.
        /// </remarks>
        /// <param name="artist"></param>
        /// <param name="XApiKey"></param>
        /// <returns>SimilarResonse[]</returns>
        [HttpGet]
        [Route("/similar")]
        [ProducesResponseType(typeof(ApiResult<SimilarResonse[]>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSimilarAsync(string artist, [FromHeader] string XApiKey)
        {
            var A = await _artist.GetSimilar(artist);
            return A.Count == 0 ? NotFound(GetApiResult("Similar", A, success: false)) : Ok(A);
        }

        /// <summary>
        /// Get topalbums artist/group
        /// </summary>
        /// <remarks>
        /// Returns all topalbums for artist/group.
        /// </remarks>
        /// <param name="artist"></param>
        /// <returns>TopAlbumsResponse</returns>
        [HttpGet]
        [Route("/topalbums")]
        [ProducesResponseType(typeof(ApiResult<List<TopAlbumsResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<TopAlbumsResponse>> GetTopAlbumsAsync(string artist, [FromHeader] string XApiKey)
        {
            var A = await _artist.GetTopAlbums(artist);
            return A;
        }

        /// <summary>
        /// Get Pictures artist/group
        /// </summary>
        /// <remarks>
        /// Returns all pictures for artist/group.
        /// </remarks>
        /// <param name="artist"></param>
        /// <returns>(Pictures[]</returns>
        [HttpGet]
        [Route("/pictures")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Pictures>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public List<Pictures> GetAsync(string artist, [FromHeader] string XApiKey)
        {
            System.Net.WebClient wc = new();
            List<Pictures> B = new();
            HtmlDocument doc = new();

            string address = "https://www.google.com/search?q=" + artist + "&tbm=isch&gws_rd=cr&ei=16E0WMGSKYmisAHmp6b4Ag";
            doc.Load(wc.OpenRead(address));
            HtmlNodeCollection imgs = doc.DocumentNode.SelectNodes("//img[@src]");
            foreach (HtmlNode img in imgs)
            {
                if (img.Attributes["src"] == null)
                    continue;
                if (img.Attributes["src"].Value.StartsWith("http"))
                {
                    HtmlAttribute src = img.Attributes["src"];
                    Pictures b = new();
                    b.ImageUrl = src.Value;
                    B.Add(b);

                }
            }

            return B;
        }

        /// <summary>
        /// Get artist/group from Spotify
        /// </summary>
        /// <remarks>
        /// Returns all Albums an track for artist/group.
        /// </remarks>
        /// <param name="artist"></param>
        /// <returns>SpotifyArtistRespone</returns>
        [HttpGet]
        [Route("/spotifyartist")]
        public async Task<List<SpotifyArtistRespone>> GetSpotifyArtist(string artist, [FromHeader] string XApiKey) 
        {
            var A = await _artist.GetSpotifyArtist(artist);
            var aa = A.albums.items;
            List<SpotifyArtistRespone> LS = new List<SpotifyArtistRespone>();
            foreach (var item in aa)
            {
                SpotifyArtistRespone sr = new SpotifyArtistRespone
                {
                    AlbumName = item.data.name,
                    AlbumCover = item.data.coverArt.sources[2].url,
                    AlbumId = item.data.uri
                };
                LS.Add(sr);
            }
            return LS;
        }

        /// <summary>
        /// Get album from Spotify
        /// </summary>
        /// <remarks>
        /// Returns all tracks from album with a preview_url.
        /// </remarks>
        /// <param name="albumId"></param>
        /// <returns>List<AlbumsSongsRespone<>/returns>
        [HttpGet]
        [Route("/spotifyalbum")]
        public async Task<List<AlbumsSongsRespone>> GetSpotifyAlbum(string albumId, [FromHeader] string XApiKey) 
        {
            var A = await _artist.GetSpotifyAlbum(albumId);
            var aa = A.albums[0].tracks.items;
            List<AlbumsSongsRespone> ls = new List<AlbumsSongsRespone>();
            foreach (var item in aa)
            {
                AlbumsSongsRespone a = new AlbumsSongsRespone
                {
                    SongName = item.name,
                    SongUri = item.preview_url
                };
                ls.Add(a);
            }
            return ls;
        }
    }
}
