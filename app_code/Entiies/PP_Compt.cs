using PetaPoco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_compt")]
public class PP_Compt : EntityBase
{
    [Display(Name = "en:Key|vi:Khóa")]
    public string ComptKey { get; set; }

    [Display(Name = "en:Type|vi:Phân loại")]
    public string ComptType { get; set; }

    [Display(Name = "en:Component name|vi:Tên component")]
    public string ComptName { get; set; }

    public string NodeType { get; set; }

    public string PageType { get; set; }

    [Display(Name = "en:Path postfix|vi:Hậu tố đường dẫn")]
    public string PathPostfix { get; set; }

    [Display(Name = "en:Schema|vi:Lược đồ")]
    public string JsonSchema { get; set; }

    [Display(Name = "en:Default|vi:Mặc định")]
    public string JsonDefault { get; set; }
}

[NotMapped]
public class ComptWithLangs : PP_Compt
{
    [Display(Name = "en:Langs|vi:Ngôn ngữ")]
    public string Langs { get; set; }
}