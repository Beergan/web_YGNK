using PetaPoco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_advise")]
public class PP_Advise : EntityBase
{
    public string Status { get; set; }

    public string Name { get; set; }

    public string Phone { get; set; }

    //public string Email { get; set; }

    //public string Address { get; set; }

    public string Content { get; set; }

    public string ProcessNote { get; set; }
}