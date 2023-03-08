using Rockhub_backend.Models.Spotify;
using RockHub_Backend.Models.Artists;
using RockHub_Backend.Models.Artists.SimilarArtists;
using RockHub_Backend.Models.Artists.TopAlbums;

namespace RockHub_Backend.Services.Interfaces
{
    public interface IArtist
    {
        Task<ArtistResponse> GetArtist(string artist);
        Task<List<SimilarResonse>> GetSimilar(string artist);
        Task<List<TopAlbumsResponse>> GetTopAlbums(string artist);
        Task<SpotifyArtist> GetSpotifyArtist(string artist);
        Task<SpotifyAlbum> GetSpotifyAlbum(string id);
    }
}
