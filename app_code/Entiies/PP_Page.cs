using PetaPoco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_page")]
public class PP_Page : EntityBase
{
    [Display(Name = "en:Language|vi:Ngôn ngữ")]
    public string LangId { get; set; }

    [Display(Name = "en:Type|vi:Phân loại")]
    public string PageType { get; set; }

    [Display(Name = "en:Type|vi:Phân loại")]
    public string NodeType { get; set; }

    [Display(Name = "en:Status|vi:Trạng thái")]
    public string PageStatus { get; set; }

    [Display(Name = "en:Title|vi:Tiêu đề")]
    public string Title { get; set; }

    [Display(Name = "en:Path|vi:Đường dẫn")]
    public string PathPattern { get; set; }

    [Display(Name = "en:Template page|vi:Mẫu hiển thị")]
    public string ComptKey { get; set; }

    [Display(Name = "en:Template page|vi:Mẫu hiển thị")]
    public string ComptName { get; set; }

    //[Display(Name = "en:Content|vi:Nội dung")]
    //public string Content { get; set; }

    public string MetaDescription { get; set; }

    public string MetaKeywords { get; set; }
}