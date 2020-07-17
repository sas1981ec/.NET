using SQLite;

namespace MusicStore.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Identificacion { get; set; }
        [NotNull]
        public string Nombres { get; set; }
        [NotNull]
        public string Apellidos { get; set; }
        [NotNull, Unique]
        public string Correo { get; set; }
        [NotNull]
        public string Contraseña { get; set; }
    }
}
