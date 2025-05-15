using SLK.Common;

public class VM_Blog : VM_Base
{
    [Field(
       Title = "en:Banner|vi:Banner",
       Required = false,
       Control = InputControlType.Image)]
    public string BannerPost { get; set; }

    [Field(
        Title = "en:Content|vi:Nội dung",
        Required = false,
        Control = InputControlType.RichTextBox)]
    public string Content { get; set; }

    //[Field(
    //   Title = "en:Time|vi:Thời gian",
    //   Required = false,
    //   Control = InputControlType.TextBox)]
    //public string TimeLine { get; set; }

    public int ? idnode { get; set; }
    public int? catid { get; set;}

    public bool Featured { get; set; }
	public string listcat { get; set; }

}