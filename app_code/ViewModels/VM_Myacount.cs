using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VM_Myacount
/// </summary>
public class VM_Myacount
{
    [Display(Name = "vi:Trạng thái")]
    public bool Active { get; set; }
    [Display(Name = "vi:tên doanh nghiệp")]
    public string Name { get; set; }
    [Display(Name = "vi:Họ và Tên")]

    public string FullName { get; set; }
    [Display(Name = "vi:Địa chỉ")]
    public string Adress { get; set; }
    [Display(Name = "vi:Tài khoản")]
    public string acount { get; set; }
    [Display(Name = "vi:Tỉnh/Thành phố")]
    public string City { get; set; }
    [Display(Name = "vi:Số điện thoại")]
    public string Phone { get; set; }

    [Display(Name = "vi:Ngành nghề")]
    public string instro { get; set; }

    [Display(Name = "vi:Email")]
    public string Email { get; set; }
    [Display(Name = "vi:Mật khẩu")]
    public string PASSWORD { get; set; }
    [Display(Name = "vi:Họ và Tên")]
    public string ConfirmPassword { get; set; }
    [Display(Name = "vi:Họ và Tên")]
    public string IdFacebook { get; set; }
    [Display(Name = "vi:idGoogle")]
    public string IdGoogle { get; set; }
    [Display(Name = "vi:Trang thái")]

    public string Status { get; set; }
    [Display(Name = "vi:Mô tả")]
    public string ProcessNote { get; set; }
}