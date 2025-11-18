using ApiSpotify.MODELS;

public class RolPermisos
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid RolId { get; set; }
    public Guid PermisosId { get; set; }
}