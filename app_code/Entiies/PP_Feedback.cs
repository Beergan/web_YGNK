using PetaPoco;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_feedback")]
public class PP_Contact : EntityBase
{
    public string Status { get; set; }


    public string Name { get; set; }
	public string Message { get; set; }
	public string Email { get; set; }

    public string Phone { get; set; }

    public string ProcessNote { get; set; }
}