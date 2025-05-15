using System.ComponentModel.DataAnnotations;

public class ShoppingCartDetails
{
    public virtual PP_Product Product { get; set; }
    public string Variation { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class OrderLine
{
	public int id { get; set; }
	public string Image { get; set; }
	public bool PromotionEnabled { get; set; }

    [Display(Name = "en:Product name|vi:Tên nhãn hiệu")]
    public string Brand { get; set; }

    [Display(Name = "en:Title|vi:Tiêu đề")]
    public string Title { get; set; }

    [Display(Name = "en:Variation|vi:Phân loại")]
    public string Variation { get; set; }

    [Display(Name = "en:Quantity|vi:Số lượng")]
    public int Quantity { get; set; }

    [Display(Name = "en:Price|vi:Giá")]
    public decimal Price { get; set; }

    [Display(Name = "en:Product weight|vi:Trọng lượng hàng")]
    public int Weight { get; set; }

    public decimal RowTotal { get; set; }
}