using PetaPoco;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_evaluation")]
public class PP_Evaluation : EntityBase
{
    public string Status { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Message { get; set; }

    public string ProcessNote { get; set; }
}