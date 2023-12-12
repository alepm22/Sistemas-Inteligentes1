using Spotify.Models.db;

namespace Spotify.Models.db
{
    public class IndexViewModel
    {
        public List<Playlist> Playlists { get; set; }
        public List<Cancion> RandomSongs { get; set; }
    }
}