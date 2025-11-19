using ApiSpotify.MODELS;

public class UsersRols
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid RolId { get; set; }
}
