using System;
using System.Collections.Generic;

namespace Spotify.Models.db
{
    public partial class Usuario
    {
        public Usuario()
        {
            Playlists = new HashSet<Playlist>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;

        public virtual ICollection<Playlist> Playlists { get; set; }
    }
}
