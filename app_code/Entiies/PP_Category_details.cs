using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PP_Category_details
/// </summary>
/// 
[Table("pp_category_details")]
public class PP_Category_details:EntityBase
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

	public int? Idproduct { get; set; }
	public int? Idcat { get; set; }
	}