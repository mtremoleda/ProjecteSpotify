namespace ApiSpotify.MODELS
{
    public class PlaylistSong
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdSong { get; set; }
        public Guid IdPlaylist { get; set; }
    }
}
