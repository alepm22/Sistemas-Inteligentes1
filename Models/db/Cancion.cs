using System;
using System.Collections.Generic;

namespace Spotify.Models.db
{
    public partial class Cancion
    {
        public Cancion()
        {
            Idplaylists = new HashSet<Playlist>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Interprete { get; set; }= null!;
        public string Album { get; set; }= null!;
        public string Genero { get; set; }= null!;
        public int Anio { get; set; }
        public string Portada { get; set; }= null!;

        public virtual ICollection<Playlist> Idplaylists { get; set; }
    }
}
