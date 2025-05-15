using System;
using System.ComponentModel.DataAnnotations;

public class EntityBase
{
    public int Id { get; set; }

    [Display(Name = "Ngày tạo")]
    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public string CreatedBy { get; set; } = "SQL";

    public string ModifiedBy { get; set; } = "SQL";

    [Display(Name = "en:Modified Time|vi:Ngày hiệu chỉnh")]
    public DateTime ModifiedTime { get; set; } = DateTime.Now;
}