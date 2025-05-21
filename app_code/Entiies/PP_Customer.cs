using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PP_Customer
/// </summary>
/// 
[Table("pp_customer")]
public class PP_Customer :EntityBase
{
    [Display(Name = "vi:Trạng thái")]
    public bool Active { get; set; }
    [Display(Name = "vi:tên doanh nghiệp")]
    public string Name { get; set; }
    [Display(Name = "vi:Họ và Tên")]

    public string FullName { get; set; }
    [Display(Name = "vi:Địa chỉ")]
    public string Adress { get; set; }
    [Display(Name = "vi:Tỉnh/Thành phố")]
    public string Provinces { get; set; }
    [Display(Name = "vi:Thành phố, Huyện")]
    public string Districts { get; set; }
    [Display(Name = "vi:Xã")]
    public string wards { get; set; }
    [Display(Name = "vi:Số điện thoại")]

    public int ProvincesId { get; set; }
    [Display(Name = "vi:Thành phố, Huyện")]
    public int DistrictsId { get; set; }
    [Display(Name = "vi:Xã")]
    public int wardsId { get; set; }
    [Display(Name = "vi:Số điện thoại")]
    public string Phone { get; set; }

    [Display(Name = "vi:Email")]
    public string Email { get; set; }
    [Display(Name = "vi:Mật khẩu")]
    public string PASSWORD { get; set; }


}