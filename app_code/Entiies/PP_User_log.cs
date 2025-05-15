using PetaPoco;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_user_log")]
public class PP_User_log : EntityBase
{
    public string Surname { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string DisplayName { get; set; }

}