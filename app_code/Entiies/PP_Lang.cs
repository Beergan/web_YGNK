using PetaPoco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_lang")]
[PrimaryKey("Id")]
public class PP_Lang : EntityBase
{
    [Display(Name = "en:LangId|vi:Mã ngôn ngữ")]
    public string LangId { get; set; }

    [Display(Name = "en:Language name|vi:Tên ngôn ngữ")]
    public string Title { get; set; }

    [Display(Name = "en:Date format|vi:Định dạng ngày")]
    public string DateFormat { get; set; }

    [Display(Name = "en:Time format|vi:Định dạng giờ")]
    public string TimeFormat { get; set; }

    [Display(Name = "en:Enabled|vi:Bật/tắt")]
    public bool Enabled { get; set; }

    [Display(Name = "en:Default|vi:Mặc định")]
    public bool IsPrimary { get; set; }
}