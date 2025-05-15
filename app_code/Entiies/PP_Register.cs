using PetaPoco;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_register")]
public class PP_Register : EntityBase
{
    public string Status { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Message { get; set; }

    public string ProcessNote { get; set; }
}