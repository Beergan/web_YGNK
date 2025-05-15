using PetaPoco;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_comment")]
public class PP_Comment : EntityBase
{
    public string Status { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Comment { get; set; }

    public string ProcessNote { get; set; }


    public string notetype {   get; set;  }

    public int idproduct { get; set; }
    public int iduser { get; set;}

	public int star { get; set; }
}