using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_order")]
public class PP_Order : EntityBase
{
    public int Date { get; set; }

    public string OrderCode { get; set; }

    [Display(Name = "en:Status|vi:Trạng thái")]
    public string OrderStatus { get; set; }

    public double TotalWeight { get; set; }

    [Display(Name = "en:Ship fee|vi:Phí ship")]
    public decimal ShipFee { get; set; }

    [Display(Name = "Tổng tiền hàng")]
    public decimal SubTotalAmount { get; set; }

    [Display(Name = "Tổng cộng")]
    public decimal TotalAmount { get; set; }

    [Display(Name = "Thanh toán")]
    public string PayMethod { get; set; }

    public string Name { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Note { get; set; }

    public string JsonData { get; set; }

    public string ReasonForCancel { get; set; }

    public string IpAddress { get; set; }
	public string Province { get; set; }
	public string District { get; set; }
	public string Ward { get; set; }

	public string DeliveryAddress { get; set; }
}