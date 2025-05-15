using PetaPoco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_config")]
public class PP_Config : EntityBase
{
    [Display(Name = "en:Language|vi:Ngôn ngữ")]
    public string LangId { get; set; }

    public int PageId { get; set; }

    public string ConfigKey { get; set; }

    public string JsonContent { get; set; }
}