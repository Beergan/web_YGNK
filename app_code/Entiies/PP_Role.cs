using PetaPoco;
using System.ComponentModel.DataAnnotations.Schema;

[Table("role")]
public class Role : EntityBase
{
    public bool Enabled { get; set; }

    public string UserId { get; set; }

    public string DisplayName { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }
}