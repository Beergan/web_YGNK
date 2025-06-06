﻿using PetaPoco;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_product")]
public class PP_Product : EntityBase
{
    [Display(Name = "en:Language|vi:Ngôn ngữ")]
    public string LangId { get; set; }

    [Display(Name = "en:Page template|vi:Mẫu hiển thị")]
    public int PageId { get; set; }

    [Display(Name = "en:Category|vi:Chuyên mục")]
    public int? CategoryId { get; set; }

    [Display(Name = "en:Type|vi:Phân loại")]
    public string NodeType { get; set; }

    [Display(Name = "en:Status|vi:Trạng thái")]
    public string NodeStatus { get; set; }

    [Display(Name = "en:Title|vi:Tiêu đề")]
    public string Title { get; set; }

    [Display(Name = "en:Path|vi:Đường dẫn")]
    public string NodePath { get; set; }

    [Display(Name = "en:Product name|vi:Tên nhãn hiệu")]
    public string Brand { get; set; }

    [Display(Name = "en:Mô tả sản phẩm|vi:Mô tả sản phẩm")]
    public string Content { get; set; }

	[Display(Name = "en:Mô tả sản phẩm|vi:Thông tin sản phẩm")]
	public string Des { get; set; }

	[Display(Name = "en:Attributes enabled|vi: Bật/tắt thuộc tính phân loại")]
    public bool AttrbEnabled { get; set; }

    [Display(Name = "en:Attribute name|vi:Tên thuộc tính")]
    public string AttrbName { get; set; }

    [Display(Name = "en:Attribute values|vi:Giá trị thuộc tính")]
    public string AttrbValues { get; set; }

    [Display(Name = "en:Price|vi:Giá")]
    public decimal Price { get; set; }

    [Display(Name = "en:Promotion price|vi:Giá khuyến mãi")]
    public decimal PromotionPrice { get; set; }

    [Display(Name = "en:Promotion label|vi:Nhãn khuyến mãi")]
    public string PromotionLabel { get; set; }

    [Display(Name = "en:Promotion enabled|vi:Kích hoạt khuyến mãi")]
    public bool PromotionEnabled { get; set; }

    [Display(Name = "en:Promotion expiration|vi:Thời hạn khuyến mãi")]
    public DateTime? PromotionExpiration { get; set; }

    [Display(Name = "en:Best seller|vi:Sản phẩm bán chạy")]
    public bool BestSeller { get; set; }

    [Display(Name = "en:Product code|vi:Mã sản phẩm")]
    public string ProductCode { get; set; }

    [Display(Name = "en:Stock qty|vi:Số lượng tồn")]
    public int StockQty { get; set; }

    [Display(Name = "en:Product weight|vi:Trọng lượng hàng", Prompt = "Tính bằng gram")]
    public int Weight { get; set; }

    [Display(Name = "en:View counter|vi:Lượt xem")]
    public int ViewCounter { get; set; }

    [Display(Name = "en:Sold counter|vi:Lượt bán")]
    public int SoldCounter { get; set; }

    public string MetaDescription { get; set; }

    public string MetaKeywords { get; set; }

    [Display(Name = "en:Image|vi:Ảnh đại diện")]
    public string ImageUrl { get; set; }
	[Display(Name = "en:Image|vi:Dãy ảnh")]
	public string ImagesJson { get; set; }
    public string listcat { get; set; }

	[NotMapped]
    [Ignore]
    public string Breadcrumb { get; set; }

    public PP_Product SetBreadcrumb(string b)
    {
        this.Breadcrumb = b;
        return this;
    }
}
