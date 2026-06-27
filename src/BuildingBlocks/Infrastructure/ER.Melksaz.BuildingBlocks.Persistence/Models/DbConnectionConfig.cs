namespace ER.Melksaz.BuildingBlocks.Persistence.Models;

public sealed class DbConnectionConfig
{
    public string ConnectionString { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public override string ToString() => $"{this.ConnectionString};user id={this.UserId};password={this.Password}";
}