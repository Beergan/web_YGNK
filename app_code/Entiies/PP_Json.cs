using PetaPoco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_json")]
[PrimaryKey("JsonKey", AutoIncrement = false)]
public class PP_Json
{
    [Key]
    public string JsonKey { get; set; }

    public string JsonContent { get; set; }
}