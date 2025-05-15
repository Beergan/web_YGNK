using PetaPoco;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_visit")]
public class PP_Visit : EntityBase
{
    public string SessionId { get; set; }

    [Display(Name = "en:Time|vi:Thời gian")]
    public int Date { get; set; }

    [Ignore]
    [NotMapped]
    public long FirstTick { get; set; }

    [Ignore]
    [NotMapped]
    public long LastTick { get; set; } = 0L;

    [Ignore]
    [NotMapped]
    public long HeartBeat { get; set; } = 0L;

    [Ignore]
    [NotMapped]
    public List<KeyValuePair<string, long>> Urls { get; set; }

    public string LastUrl { get; set; }

    [Display(Name = "en:Country|vi:Quốc gia")]
    public string Country { get; set; }

    [Display(Name = "Referer")]
    public string Referer { get; set; }

    [Display(Name = "en:Device|vi:Thiết bị")]
    public string Device { get; set; }

    [Display(Name = "en:Browser|vi:Trình duyệt")]
    public string Browser { get; set; }

    [Display(Name = "en:Ip|vi:Địa chỉ ip")]
    public string Ip { get; set; }

    public int ClickCount { get; set; }

    [Display(Name = "en:Make order|vi:Đặt hàng")]
    public bool MakeOrder { get; set; }
 
    public int StayTime { get; set; }

    public string JsonDetails { get; set; }
}