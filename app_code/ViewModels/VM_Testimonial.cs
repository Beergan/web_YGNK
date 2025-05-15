using SLK.Common;

public class VM_Testimonial 
{
    [Field(
        Title = "en:Name|vi:Tên",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Title { get; set; }

    [Field(
        Title = "en:Content|vi:Nội dung",
        Required = false,
        Control = InputControlType.TextArea)]
    public string Content { get; set; }

    [Field(
        Title = "en:Image|vi:Hình đại diện",
        Required = false,
        Control = InputControlType.Image)]
    public string Image { get; set; }
}