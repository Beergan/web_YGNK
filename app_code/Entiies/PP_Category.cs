using PetaPoco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_category")]
public class PP_Category : EntityBase
{
    [Display(Name = "en:Languague|vi:Ngôn ngữ")]
    public string LangId { get; set; }

    [Display(Name = "en:Category|vi:Chuyên mục")]
    public int? ParentId { get; set; }

    [Display(Name = "en:List template|vi:Mẫu hiển thị danh sách")]
    public int PageId { get; set; }

    [Display(Name = "en:Item template|vi:Mẫu hiển thị phần tử")]
    public int PageIdItem { get; set; }

    [Display(Name = "en:Type|vi:Phân loại")]
    public string NodeType { get; set; }

    public int CategoryLevel { get; set; }

    [Display(Name = "en:Title|vi:Tiêu đề")]
    public string Title { get; set; }

    [Display(Name = "en:STT|vi:STT")]
    public int STT { get; set; }

    [Display(Name = "en:Breadcrumb|vi:Breadcrumb")]
    public string Breadcrumb { get; set; }

    [Display(Name = "en:Category path|vi:Đường dẫn")]
    public string CategoryPath { get; set; }

    [Display(Name = "en:Image|vi:Ảnh đại diện")]
    public string ImageUrl { get; set; }

    public string MetaDescription { get; set; }

    public string MetaKeywords { get; set; }
}