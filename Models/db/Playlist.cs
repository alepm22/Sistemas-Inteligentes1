using System;
using System.Collections.Generic;

namespace Spotify.Models.db
{
    public partial class Playlist
    {
        public Playlist()
        {
            Idcancions = new HashSet<Cancion>();
        }

        public int Id { get; set; }
        public int Idusuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; }= null!;

        public virtual Usuario IdusuarioNavigation { get; set; }

        public virtual ICollection<Cancion> Idcancions { get; set; }
    }
}
