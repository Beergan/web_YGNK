using PetaPoco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_node")]
public class PP_Node : EntityBase
{
    [Display(Name = "en:Language|vi:Ngôn ngữ")]
    public string LangId { get; set; }

    [Display(Name = "en:Category|vi:Chuyên mục")]
    public int? CategoryId { get; set; }

    [Display(Name = "en:Page template|vi:Mẫu hiển thị")]
    public int PageId { get; set; }

    [Display(Name = "en:Type|vi:Phân loại")]
    public string NodeType { get; set; }

    [Display(Name = "en:Status|vi:Trạng thái")]
    public string NodeStatus { get; set; }

    [Display(Name = "en:Title|vi:Tiêu đề")]
    public string Title { get; set; }

    [Display(Name = "en:Path|vi:Đường dẫn")]
    public string NodePath { get; set; }

    [Display(Name = "en:Featured|vi:Nổi bật")]
    public bool Featured { get; set; }

    [Display(Name = "en:Summary|vi:Tóm tắt")]
    public string Summary { get; set; }

    [Display(Name = "en:Content|vi:Nội dung")]
    public string Content { get; set; }

    public string MetaDescription { get; set; }

    public string MetaKeywords { get; set; }

    [Display(Name = "en:Image|vi:Ảnh đại diện")]
    public string ImageUrl { get; set; }

	public string listcat { get; set; }

	[NotMapped]
    [Ignore]
    public string Breadcrumb { get; set; }

    public PP_Node SetBreadcrumb(string b) {
        this.Breadcrumb = b;
        return this;
    }
}